using BimStructure.Dtos;

namespace BimStructure.Repository;

public interface IUnitRepository
{
    Task<ProgramControlDto> GetProgramControlAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
