using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public sealed class GridRepository : IGridRepository
{
    private const string GridDefinitionsQuery =
        "SELECT [ID], [Grid Line Type], [Ordinate] FROM [Grid Definitions - Grid Lines]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public GridRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public IReadOnlyList<GridLineDto> GetGridLines(string databasePath)
    {
        return _queryExecutor.Query(databasePath, GridDefinitionsQuery, GridLineMapper.Map);
    }
}
