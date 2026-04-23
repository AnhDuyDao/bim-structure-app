using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class PointBaysRepository : RepositoryBase, IPointBaysRepository
{
    private const string Query =
        "SELECT [Label], [X], [Y] FROM [Point Bays]";

    public PointBaysRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<PointBaysRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<PointBayDto>> GetPointBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            PointBayMapper.Map,
            cancellationToken);
    }
}