using System.Data;
using System.Data.OleDb;

namespace BimStructure.Repositories;

public interface IAccessQueryExecutor
{
    Task<IReadOnlyList<T>> QueryAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        IEnumerable<OleDbParameter>? parameters = null,
        CancellationToken cancellationToken = default);

    Task<T?> QuerySingleOrDefaultAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        IEnumerable<OleDbParameter>? parameters = null,
        CancellationToken cancellationToken = default);
}
