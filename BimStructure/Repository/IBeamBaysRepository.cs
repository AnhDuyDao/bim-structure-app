using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IBeamBaysRepository
{
    IReadOnlyList<MemberBayDto> GetBeamBays(string databasePath);
}