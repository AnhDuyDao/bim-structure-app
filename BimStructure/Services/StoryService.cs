using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class StoryService : IStoryService
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
        var baseStories = _baseStoryRepository.GetBaseStories(databasePath);
        var stories = _storyRepository.GetStories(databasePath);

        if (baseStories.Count == 0)
        {
            throw new InvalidOperationException("Khong tim thay base story.");
        }

        if (stories.Count == 0)
        {
            throw new InvalidOperationException("Khong tim thay story definitions.");
        }

        var baseStory = baseStories.First();
        var result = new DBStory[stories.Count];
        var currentElevation = baseStory.Elevation;

        for (var i = stories.Count - 1; i >= 0; i--)
        {
            var story = stories[i];
            currentElevation += story.Height;

            result[i] = new DBStory
            {
                Name = story.Name,
                Height = story.Height,
                Elevation = currentElevation
            };
        }

        return result
            .Append(new DBStory
            {
                Name = baseStory.Name,
                Height = 0d,
                Elevation = baseStory.Elevation
            })
            .ToList();
    }
}
