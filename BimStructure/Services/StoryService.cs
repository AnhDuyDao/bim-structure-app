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
        var baseStories = _baseStoryRepository.GetBaseStory(databasePath);
        var stories = _storyRepository.GetStories(databasePath);

        var baseStory = baseStories.First();
        
        stories.Add(baseStory);
        
        double currentElevation = stories.Last().Elevation;
        
        for(int i = stories.Count - 1; i >= 0; i--)
        {
            var story = stories[i];
            currentElevation += story.Height;
            story.Elevation = currentElevation;
        }
        return stories;
    }
}