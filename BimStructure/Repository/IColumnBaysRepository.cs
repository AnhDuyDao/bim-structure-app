using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IColumnBaysRepository
{
    IReadOnlyList<MemberBayDto> GetColumnBays(string databasePath);
}