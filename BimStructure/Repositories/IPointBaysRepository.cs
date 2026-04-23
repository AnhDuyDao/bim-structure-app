
using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IPointBaysRepository
{
    Task<IReadOnlyList<PointBayDto>> GetPointBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}