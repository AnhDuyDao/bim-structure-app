
using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public class PointBaysRepository : IPointBaysRepository
{
    private const string PointBaysQuery = "SELECT [Label], [X], [Y] FROM [Point Bays]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public PointBaysRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IReadOnlyList<PointBayDto> GetPointBays(string databasePath)
    {
        return _queryExecutor.Query(databasePath, PointBaysQuery, PointBayMapper.Map);
    }
}