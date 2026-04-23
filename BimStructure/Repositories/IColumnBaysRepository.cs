using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IColumnBaysRepository
{
    Task<IReadOnlyList<MemberBayDto>> GetColumnBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}