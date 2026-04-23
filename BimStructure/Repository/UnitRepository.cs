using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class UnitRepository : RepositoryBase, IUnitRepository
{
    private const string Query =
        "SELECT [CurrUnits] FROM [Program Control]";

    public UnitRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<UnitRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<ProgramControlDto> GetProgramControlAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteSingleAsync(
            databasePath,
            Query,
            ProgramControlMapper.Map,
            cancellationToken);
    }
}