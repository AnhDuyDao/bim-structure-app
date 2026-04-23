using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IStoryRepository
{
    Task<IReadOnlyList<StoryDefinitionDto>> GetStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
