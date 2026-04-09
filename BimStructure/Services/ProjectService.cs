using BimStructure.Models;

namespace BimStructure.Services;

public class ProjectService : IProjectService
{
    public Project? CurrentProject { get; private set; }

    public void CreateProject(Project project)
    {
        CurrentProject = project;
    }
}