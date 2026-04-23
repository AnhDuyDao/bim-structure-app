using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IColumnBaysRepository
{
    Task<IReadOnlyList<MemberBayDto>> GetColumnBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}