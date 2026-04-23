using BimStructure.Repository.Dtos;
using BimStructure.Repository.Mappers;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repository;

public sealed class BeamBaysRepository : RepositoryBase, IBeamBaysRepository
{
    private const string Query =
        "SELECT [Label], [PointBayI], [PointBayJ] FROM [Beam Bays]";

    public BeamBaysRepository(
        IAccessQueryExecutor queryExecutor,
        ILogger<BeamBaysRepository> logger)
        : base(queryExecutor, logger)
    {
    }

    public Task<IReadOnlyList<MemberBayDto>> GetBeamBaysAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        return ExecuteQueryAsync(
            databasePath,
            Query,
            MemberBayMapper.Map,
            cancellationToken);
    }
}