using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public sealed class UnitRepository : IUnitRepository
{
    private const string ProgramControlQuery = "SELECT [CurrUnits] FROM [Program Control]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public UnitRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public ProgramControlDto GetProgramControl(string databasePath)
    {
        return _queryExecutor.QuerySingleOrDefault(databasePath, ProgramControlQuery, ProgramControlMapper.Map)
            ?? throw new InvalidOperationException("Khong tim thay du lieu trong bang [Program Control].");
    }
}
