using System.Data;

namespace BimStructure.Services;

public interface IAccessDatabaseService
{
    void ValidateConnection(string databasePath);
    DataTable ExecuteQuery(string databasePath, string queryString);
}
