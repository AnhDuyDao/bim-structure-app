using BimStructure.Models;

namespace BimStructure.Services;

public interface IStoryService
{
    List<DBStory> GetAllStories(string databasePath);
}