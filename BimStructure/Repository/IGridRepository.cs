using BimStructure.Models;

namespace BimStructure.Repository;

public interface IGridRepository
{
    Dictionary<string, DBGrid> GetGrids(string databasePath);
}
