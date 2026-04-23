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

    public Dictionary<string, DBGrid> GetGrids(string databasePath)
    {
        var gridLines = _gridRepository.GetGridLines(databasePath);
        return gridLines.ToDictionary(
            line => line.Id,
            line => new DBGrid
            {
                Name = line.Id,
                Direction = line.GridLineType == "X (Cartesian)"
                    ? GridDirection.X
                    : GridDirection.Y,
                Coordinate = line.Ordinate
            });
    }
}
