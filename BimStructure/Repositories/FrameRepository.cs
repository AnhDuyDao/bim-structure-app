using BimStructure.Dtos;
using BimStructure.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repositories;

public class FrameRepository : RepositoryBase, IFrameRepository
{
    private const string Query = @"
        SELECT [Label], [Story], [Length], [Analysis Section], [Design Type]
        FROM [Frame Assignments - Summary]
        WHERE [Design Type] IN ('Column', 'Beam')";

    public FrameRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<FrameRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<FrameDto>> GetFramesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            FrameMapper.Map,
            cancellationToken);
    }
}