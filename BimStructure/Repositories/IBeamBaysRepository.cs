using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IBeamBaysRepository
{
    Task<IReadOnlyList<MemberBayDto>> GetBeamBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}