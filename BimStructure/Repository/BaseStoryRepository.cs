using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public sealed class BaseStoryRepository : IBaseStoryRepository
{
    private const string StoryBaseDefinitionsQuery =
        "SELECT [BSName], [BSElev] FROM [Tower and Base Story Definitions]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public BaseStoryRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IReadOnlyList<BaseStoryDto> GetBaseStories(string databasePath)
    {
        return _queryExecutor.Query(databasePath, StoryBaseDefinitionsQuery, BaseStoryMapper.Map);
    }
}
