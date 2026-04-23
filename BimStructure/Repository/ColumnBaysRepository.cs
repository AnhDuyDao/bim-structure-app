using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public class ColumnBaysRepository : IColumnBaysRepository
{
    private const string ColumnBaysQuery = "SELECT [Label], [PointBayI], [PointBayJ] FROM [Column Bays]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public ColumnBaysRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IReadOnlyList<MemberBayDto> GetColumnBays(string databasePath)
    {
        return _queryExecutor.Query(databasePath, ColumnBaysQuery, MemberBayMapper.Map);
    }
}