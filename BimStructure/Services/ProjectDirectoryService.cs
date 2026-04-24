using System.IO;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class ProjectDirectoryService : IProjectDirectoryService
{
    private readonly IEtabsFileService _etabsFileService;
    private readonly ILogger<ProjectDirectoryService> _logger;

    public ProjectDirectoryService(ILogger<ProjectDirectoryService> logger, IEtabsFileService etabsFileService)
    {
        _logger = logger;
        _etabsFileService = etabsFileService;
    }

    public string CreateProjectStructure(string basePath, string projectName, string etabsSourceFile)
    {
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Base path is required.");

        if (string.IsNullOrWhiteSpace(projectName))
            throw new ArgumentException("Project name is required.");

        var safeName = MakeSafeFolderName(projectName);

        var projectRoot = Path.Combine(basePath, safeName);
        
        _logger.LogInformation("Creating project folder: {Folder}", projectRoot);

        CreateDir(projectRoot);
        CreateDir(Path.Combine(projectRoot, "etabs"));
        CreateDir(Path.Combine(projectRoot, "res"));
        CreateDir(Path.Combine(projectRoot, "Resources"));
        
        var outputDbPath = _etabsFileService.CopyDatabase(
            etabsSourceFile,
            projectRoot);

        _logger.LogInformation("Project structure created successfully");
        
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