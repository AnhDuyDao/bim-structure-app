using System.IO;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class ProjectDirectoryService : IProjectDirectoryService
{
    private readonly ILogger<ProjectDirectoryService> _logger;

    public ProjectDirectoryService(ILogger<ProjectDirectoryService> logger)
    {
        _logger = logger;
    }

    public string CreateProjectStructure(string basePath, string projectName)
    {
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Base path is required.");

        if (string.IsNullOrWhiteSpace(projectName))
            throw new ArgumentException("Project name is required.");

        var safeName = MakeSafeFolderName(projectName);

        var projectRoot = Path.Combine(basePath, safeName);

        CreateDir(projectRoot);
        CreateDir(Path.Combine(projectRoot, "etabs"));
        CreateDir(Path.Combine(projectRoot, "res"));
        CreateDir(Path.Combine(projectRoot, "Resources"));

        return projectRoot;
    }

    private static void CreateDir(string path)
    {
        Directory.CreateDirectory(path);
    }

    private static string MakeSafeFolderName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        foreach (var c in invalid)
        {
            name = name.Replace(c, '_');
        }

        return name.Trim();
    }
}