using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using BimStructure.Configuration;

namespace BimStructure.Repository;

public sealed class AccessRepository : IAccessRepository
{
    private readonly PluginConfiguration _configuration;

    public AccessRepository(PluginConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DataTable GetData(string databasePath, string query)
    {
        var dataTable = new DataTable();

        using var connection = new OleDbConnection(BuildConnectionString(databasePath));
        connection.Open();

        using var command = new OleDbCommand(query, connection);
        using var reader = command.ExecuteReader();

        if (reader is not null)
        {
            dataTable.Load(reader, LoadOption.OverwriteChanges);
        }

        return dataTable;
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
