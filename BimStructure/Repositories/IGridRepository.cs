using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IGridRepository
{
    Task<IReadOnlyList<GridLineDto>> GetGridLinesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
