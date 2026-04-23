using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public sealed class StoryRepository : IStoryRepository
{
    private const string StoriesDefinitionsQuery = "SELECT [Name], [Height] FROM [Story Definitions]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public StoryRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IReadOnlyList<StoryDefinitionDto> GetStories(string databasePath)
    {
        return _queryExecutor.Query(databasePath, StoriesDefinitionsQuery, StoryDefinitionMapper.Map);
    }
}
