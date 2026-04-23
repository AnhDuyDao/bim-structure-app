using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class ColumnBaysRepository : RepositoryBase, IColumnBaysRepository
{
    private const string Query =
        "SELECT [Label], [PointBayI], [PointBayJ] FROM [Column Bays]";

    public ColumnBaysRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<ColumnBaysRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<MemberBayDto>> GetColumnBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            MemberBayMapper.Map,
            cancellationToken);
    }
}