using System.Data;

namespace BimStructure.Repository;

public interface IAccessRepository
{
    void ValidateDatabase(string databasePath);
    DataTable GetData(string databasePath, string query);
}
