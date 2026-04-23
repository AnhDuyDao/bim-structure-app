using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;

namespace BimStructure.Repository;

public class SectionRepository : ISectionRepository
{
    private const string SectionsQuery = "SELECT [Name], [Depth], [Width] FROM [Frame Section Property Definitions - Concrete Rectangular]";

    private readonly IAccessQueryExecutor _queryExecutor;

    public SectionRepository(IAccessQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }
    
    public IReadOnlyList<SectionDto> GetSections(string databasePath)
    {
        return _queryExecutor.Query(databasePath, SectionsQuery, SectionMapper.Map);
    }
}