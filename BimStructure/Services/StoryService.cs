using BimStructure.Dtos;
using BimStructure.Models;
using BimStructure.Repositories;

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

    public async Task<IReadOnlyList<DBStory>> GetAllStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        var baseTask = _baseStoryRepository.GetBaseStoriesAsync(databasePath, cancellationToken);
        var storyTask = _storyRepository.GetStoriesAsync(databasePath, cancellationToken);

        await Task.WhenAll(new Task[] { baseTask, storyTask });

        var baseStories = await baseTask;
        var stories = await storyTask;

        if (baseStories.Count == 0)
            throw new InvalidOperationException("Not found base story.");

        if (stories.Count == 0)
            throw new InvalidOperationException("Not found story definitions.");

        return BuildStories(baseStories[0], stories);
    }

    private static IReadOnlyList<DBStory> BuildStories(
        BaseStoryDto baseStory,
        IReadOnlyList<StoryDefinitionDto> stories)
    {
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