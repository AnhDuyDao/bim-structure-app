using BimStructure.Models;

namespace BimStructure.Repository;

public interface IBaseStoryRepository
{
    List<DBStory> GetBaseStory(string databasePath);
}