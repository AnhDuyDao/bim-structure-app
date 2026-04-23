using System.Data;
using System.Data.OleDb;
using System.IO;
using BimStructure.Configuration;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class AccessQueryExecutor : IAccessQueryExecutor
{
    private readonly PluginConfiguration _configuration;
    private readonly ILogger<AccessQueryExecutor> _logger;

    public AccessQueryExecutor(
        PluginConfiguration configuration,
        ILogger<AccessQueryExecutor> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        IEnumerable<OleDbParameter>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var results = new List<T>();

        try
        {
            using var connection = new OleDbConnection(BuildConnectionString(databasePath));
            using var command = CreateCommand(connection, query, parameters);

            await connection.OpenAsync(cancellationToken);

            _logger.LogDebug("Executing query: {Query}", query);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader is null)
                return results;

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(map(reader));
            }

            _logger.LogInformation("Query returned {Count} rows", results.Count);

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing query: {Query}", query);
            throw;
        }
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        IEnumerable<OleDbParameter>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var results = await QueryAsync(
            databasePath,
            query,
            map,
            parameters,
            cancellationToken);

        return results.Count switch
        {
            0 => default,
            1 => results[0],
            _ => throw new InvalidOperationException("Query returned more than one result.")
        };
    }

    private OleDbCommand CreateCommand(
        OleDbConnection connection,
        string query,
        IEnumerable<OleDbParameter>? parameters)
    {
        var command = new OleDbCommand(query, connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }
        }

        return command;
    }

    private string BuildConnectionString(string databasePath)
    {
        if (string.IsNullOrWhiteSpace(databasePath))
            throw new ArgumentException("Database path is required.", nameof(databasePath));

        if (!File.Exists(databasePath))
            throw new FileNotFoundException("Access database file was not found.", databasePath);

        return string.Format(
            _configuration.AccessConnectionStringTemplate,
            _configuration.AccessProvider,
            databasePath);
    }
}