using System.Data;

namespace BimStructure.Repository;

public interface IAccessQueryExecutor
{
    IReadOnlyList<T> Query<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map);

    T? QuerySingleOrDefault<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map);
}
