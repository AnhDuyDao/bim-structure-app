using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class BaseStoryRepository : RepositoryBase, IBaseStoryRepository
{
    private const string Query =
        "SELECT [BSName], [BSElev] FROM [Tower and Base Story Definitions]";

    public BaseStoryRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<BaseStoryRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<BaseStoryDto>> GetBaseStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            BaseStoryMapper.Map,
            cancellationToken);
    }
}