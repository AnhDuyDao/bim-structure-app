using BimStructure.Models;

namespace BimStructure.Services;

public sealed class ProjectService : IProjectService
{
    public Project? CurrentProject { get; private set; }

    public Task CreateProjectAsync(
        Project project,
        CancellationToken cancellationToken = default)
    {
        if (project is null)
            throw new ArgumentNullException(nameof(project));

        CurrentProject = project;

        return Task.CompletedTask;
    }
}