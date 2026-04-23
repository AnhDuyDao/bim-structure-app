using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IBaseStoryRepository
{
    IReadOnlyList<BaseStoryDto> GetBaseStories(string databasePath);
}
