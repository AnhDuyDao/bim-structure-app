using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IBeamBaysRepository
{
    IReadOnlyList<MemberBayDto> GetColumnBays(string databasePath);
}