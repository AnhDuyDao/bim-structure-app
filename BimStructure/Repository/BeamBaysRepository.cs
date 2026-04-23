using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public class BeamBaysRepository : IBeamBaysRepository
{
    private const string BeamBaysQuery = "SELECT [Label], [PointBayI], [PointBayJ] FROM [Beam Bays]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public BeamBaysRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }
    public IReadOnlyList<MemberBayDto> GetColumnBays(string databasePath)
    {
        return _queryExecutor.Query(databasePath, BeamBaysQuery, MemberBayMapper.Map);
    }
}