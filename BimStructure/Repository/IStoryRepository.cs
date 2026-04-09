using BimStructure.Models;

namespace BimStructure.Repository;

public interface IStoryRepository
{
    List<DBStory> GetStories(string databasePath);
}