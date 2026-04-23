using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface IUnitRepository
{
    Task<ProgramControlDto> GetProgramControlAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
