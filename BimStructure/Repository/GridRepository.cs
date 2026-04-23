using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class GridRepository : RepositoryBase, IGridRepository
{
    private const string Query =
        "SELECT [ID], [Grid Line Type], [Ordinate] FROM [Grid Definitions - Grid Lines]";

    public GridRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<GridRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<GridLineDto>> GetGridLinesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            GridLineMapper.Map,
            cancellationToken);
    }
}