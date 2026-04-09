using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public class GridService : IGridService
{
    private readonly IGridRepository _gridRepository;
    public GridService(IGridRepository gridRepository)
    {
        _gridRepository = gridRepository;
    }

    public Dictionary<string, DBGrid> GetGrids(string databasePath)
    {
        var grids = _gridRepository.GetGrids(databasePath);
        return grids;
    }
}