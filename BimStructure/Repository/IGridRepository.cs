using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IGridRepository
{
    IReadOnlyList<GridLineDto> GetGridLines(string databasePath);
}
