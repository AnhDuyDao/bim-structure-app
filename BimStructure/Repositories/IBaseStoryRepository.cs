using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IBaseStoryRepository
{
    Task<IReadOnlyList<BaseStoryDto>> GetBaseStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
