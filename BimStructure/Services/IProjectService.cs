using BimStructure.Models;

namespace BimStructure.Services;

public interface IProjectService
{
    Project? CurrentProject { get; }
    void CreateProject(Project project);
}