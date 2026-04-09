using BimStructure.Models;

namespace BimStructure.Services;

public interface IGridService
{
    Dictionary<string, DBGrid> GetGrids(string databasePath);
}