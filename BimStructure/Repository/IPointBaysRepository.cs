
using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IPointBaysRepository
{
    IReadOnlyList<PointBayDto> GetPointBays(string databasePath);
}