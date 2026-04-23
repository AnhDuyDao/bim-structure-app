using BimStructure.Models;

namespace BimStructure.Services;

public interface IUnitService
{
    Task<DBUnitSet> GetUnitsAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}
