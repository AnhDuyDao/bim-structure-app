using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IStoryRepository
{
    Task<IReadOnlyList<StoryDefinitionDto>> GetStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
