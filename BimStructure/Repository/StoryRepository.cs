using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class StoryRepository : RepositoryBase, IStoryRepository
{
    private const string Query = "SELECT [Name], [Height] FROM [Story Definitions]";

    public StoryRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<StoryRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<StoryDefinitionDto>> GetStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            StoryDefinitionMapper.Map,
            cancellationToken);
    }
}
