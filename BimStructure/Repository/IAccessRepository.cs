using System.Data;

namespace BimStructure.Repository;

public interface IAccessRepository
{
    DataTable GetData(string databasePath, string query);
}
