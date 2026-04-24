namespace BimStructure.Services;

public interface IProjectDirectoryService
{
    string CreateProjectStructure(string basePath, string projectName);
}