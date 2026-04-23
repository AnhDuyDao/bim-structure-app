using BimStructure.Models;

namespace BimStructure.Services;

public interface IProjectService
{
    Project? CurrentProject { get; }

    Task CreateProjectAsync(
        Project project,
        CancellationToken cancellationToken = default);
}