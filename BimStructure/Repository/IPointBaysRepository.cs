
using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IPointBaysRepository
{
    Task<IReadOnlyList<PointBayDto>> GetPointBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}