using BimStructure.Models;

namespace BimStructure.Services;

public interface IStoryService
{
    Task<IReadOnlyList<DBStory>> GetAllStoriesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}