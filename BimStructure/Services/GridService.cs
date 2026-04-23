using BimStructure.Dtos;
using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class GridService : IGridService
{
    private readonly IGridRepository _gridRepository;

    public GridService(IGridRepository gridRepository)
    {
        _gridRepository = gridRepository;
    }

    public async Task<IReadOnlyDictionary<string, DBGrid>> GetGridsAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        var gridLines = await _gridRepository.GetGridLinesAsync(
            databasePath,
            cancellationToken);

        return gridLines.ToDictionary(
            line => line.Id,
            MapToDomain);
    }

    private static DBGrid MapToDomain(GridLineDto line)
    {
        return new DBGrid
        {
            Name = line.Id,
            Direction = line.GridLineType == "X (Cartesian)"
                ? GridDirection.X
                : GridDirection.Y,
            Coordinate = line.Ordinate
        };
    }
}