using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IGridRepository
{
    Task<IReadOnlyList<GridLineDto>> GetGridLinesAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
