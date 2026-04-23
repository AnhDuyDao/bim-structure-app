using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repositories;

public class SectionRepository : RepositoryBase, ISectionRepository
{
    private const string Query = "SELECT [Name], [Depth], [Width] FROM [Frame Section Property Definitions - Concrete Rectangular]";

    public SectionRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<SectionRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<SectionDto>> GetSectionsAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            SectionMapper.Map,
            cancellationToken);
    }
}