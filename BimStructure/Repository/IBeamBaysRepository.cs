using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IBeamBaysRepository
{
    Task<IReadOnlyList<MemberBayDto>> GetBeamBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}