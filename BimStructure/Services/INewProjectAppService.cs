using BimStructure.Models;

namespace BimStructure.Services;

public interface INewProjectAppService
{
    Task<DBUnitSet> ReadUnitsAsync(
        string accessFilePath,
        CancellationToken cancellationToken = default);

    Task CreateProjectAsync(
        CreateProjectRequest request,
        CancellationToken cancellationToken = default);
}
