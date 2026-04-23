using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IFrameRepository
{
    Task<IReadOnlyList<FrameDto>> GetFramesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}