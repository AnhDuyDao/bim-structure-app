using BimStructure.Models;

namespace BimStructure.Services;

public interface IFrameService
{
    Task<IReadOnlyList<DBFrame>> GetFramesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);

}