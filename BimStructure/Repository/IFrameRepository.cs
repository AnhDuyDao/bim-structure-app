using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IFrameRepository
{
    Task<IReadOnlyList<FrameDto>> GetFramesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}