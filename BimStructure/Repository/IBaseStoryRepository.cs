using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IBaseStoryRepository
{
    Task<IReadOnlyList<BaseStoryDto>> GetBaseStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
