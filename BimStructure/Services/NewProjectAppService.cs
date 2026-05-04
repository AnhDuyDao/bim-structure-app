using System.IO;
using BimStructure.Models;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class NewProjectAppService : INewProjectAppService
{
    private readonly IUnitService _unitService;
    private readonly IProjectService _projectService;
    private readonly IProjectDirectoryService _projectDirectoryService;
    private readonly IProjectPersistenceService _projectPersistenceService;
    private readonly ILogger<ProjectPersistenceService> _logger;

    public NewProjectAppService(
        IUnitService unitService,
        IProjectService projectService, IProjectDirectoryService projectDirectoryService, IProjectPersistenceService projectPersistenceService, ILogger<ProjectPersistenceService> logger)
    {
        _unitService = unitService;
        _projectService = projectService;
        _projectDirectoryService = projectDirectoryService;
        _projectPersistenceService = projectPersistenceService;
        _logger = logger;
    }

    public async Task<DBUnitSet> ReadUnitsAsync(
        string accessFilePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accessFilePath))
            throw new ArgumentException("Access file path is required.", nameof(accessFilePath));

        return await _unitService.GetUnitsAsync(accessFilePath, cancellationToken);
    }

    public async Task CreateProjectAsync(
        CreateProjectRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);
        
        string? projectRoot = null;

        try
        {
            // 1. Create folder structure
            projectRoot = _projectDirectoryService.CreateProjectStructure(
                request.FolderPath,
                request.ProjectName,
                request.ImportFile);

            // 2. Create domain object
            var project = new Project
            {
                Name = request.ProjectName,
                RootPath = projectRoot,
                DBFileName = request.ImportFile,
                Concrete = request.Concrete,
                Steel = request.Steel
            };

            // 3. Save project.json
            _projectPersistenceService.Save(project);

            // 4. Set current project
            await _projectService.CreateProjectAsync(project, cancellationToken);
        }
        catch (Exception ex)
        {
            // 🔥 ROLLBACK
            if (!string.IsNullOrWhiteSpace(projectRoot) && Directory.Exists(projectRoot))
            {
                try
                {
                    Directory.Delete(projectRoot, recursive: true);
                }
                catch
                {
                    // log warning nếu cần
                    _logger.LogWarning(ex, "Failed to save project");
                }
            }

            throw; 
        }
    }

    private static void ValidateRequest(CreateProjectRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.ProjectName))
            throw new ArgumentException("Project name is required.", nameof(request.ProjectName));

        if (string.IsNullOrWhiteSpace(request.FolderPath))
            throw new ArgumentException("Folder path is required.", nameof(request.FolderPath));

        if (string.IsNullOrWhiteSpace(request.ImportFile))
            throw new ArgumentException("Import file is required.", nameof(request.ImportFile));
    }
}