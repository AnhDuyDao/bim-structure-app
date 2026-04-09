using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public class StoryService : IStoryService
{
    private readonly IBaseStoryRepository _baseStoryRepository;
    private readonly IStoryRepository _storyRepository;
    
    public StoryService(
        IBaseStoryRepository baseRepository,
        IStoryRepository storyRepository)
    {
        _baseStoryRepository = baseRepository;
        _storyRepository = storyRepository;
    }
    
    public List<DBStory> GetAllStories(string databasePath)
    {
        var baseStory = _baseStoryRepository.GetBaseStory(databasePath);
        var stories = _storyRepository.GetStories(databasePath);

        var allStories = baseStory.Concat(stories).ToList();
        
        double currentElevation = allStories.First().Elevation;

        for (int i = 1; i < allStories.Count; i++)
        {
            currentElevation += allStories[i].Height;
            allStories[i].Elevation = currentElevation;
        }

        return allStories;
    }
}