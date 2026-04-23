using BimStructure.Models;

namespace BimStructure.Services;

public interface IGridService
{
    Task<IReadOnlyDictionary<string, DBGrid>> GetGridsAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}