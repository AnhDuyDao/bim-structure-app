using System.Data;
using System.Data.OleDb;
using System.IO;
using BimStructure.Configuration;

namespace BimStructure.Repository;

public sealed class AccessQueryExecutor : IAccessQueryExecutor
{
    private readonly PluginConfiguration _configuration;

    public AccessQueryExecutor(PluginConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IReadOnlyList<T> Query<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map)
    {
        var results = new List<T>();

        using var connection = new OleDbConnection(BuildConnectionString(databasePath));
        using var command = new OleDbCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();
        if (reader is null)
        {
            return results;
        }

        while (reader.Read())
        {
            results.Add(map(reader));
        }

        return results;
    }

    public T? QuerySingleOrDefault<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map)
    {
        using var connection = new OleDbConnection(BuildConnectionString(databasePath));
        using var command = new OleDbCommand(query, connection);

        connection.Open();

        using var reader = command.ExecuteReader();
        if (reader is null || !reader.Read())
        {
            return default;
        }

        var result = map(reader);
        if (reader.Read())
        {
            throw new InvalidOperationException("Query return more than once.");
        }

        return result;
    }

    private string BuildConnectionString(string databasePath)
    {
        if (string.IsNullOrWhiteSpace(databasePath))
        {
            throw new ArgumentException("Database path is required.", nameof(databasePath));
        }

        if (!File.Exists(databasePath))
        {
            throw new FileNotFoundException("Access database file was not found.", databasePath);
        }

        return string.Format(
            _configuration.AccessConnectionStringTemplate,
            _configuration.AccessProvider,
            databasePath);
    }
}
