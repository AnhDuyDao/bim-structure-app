using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IStoryRepository
{
    IReadOnlyList<StoryDefinitionDto> GetStories(string databasePath);
}
