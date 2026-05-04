using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BimStructure.Models;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class NewProjectAppService : INewProjectAppService
{
    private readonly IUnitService _unitService;
    private readonly IProjectService _projectService;
    private readonly IProjectDirectoryService _projectDirectoryService;
    private readonly IProjectPersistenceService _projectPersistenceService;
    
    private readonly ILogger<NewProjectAppService> _logger;

    public NewProjectAppService(
        IUnitService unitService,
        IProjectService projectService,
        IProjectDirectoryService projectDirectoryService,
        IProjectPersistenceService projectPersistenceService,
        ILogger<NewProjectAppService> logger)
    {
        _unitService = unitService;
        _projectService = projectService;
        _projectDirectoryService = projectDirectoryService;
        _projectPersistenceService = projectPersistenceService;
        _logger = logger;
    }

    // =========================
    // READ UNITS
    // =========================
    public async Task<DBUnitSet> ReadUnitsAsync(
        string accessFilePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accessFilePath))
            throw new ArgumentException("Access file path is required.", nameof(accessFilePath));

        return await _unitService.GetUnitsAsync(accessFilePath, cancellationToken);
    }

    // =========================
    // CREATE PROJECT
    // =========================
    public async Task CreateProjectAsync(
        CreateProjectRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);

        string? projectRoot = null;

        try
        {
            _logger.LogInformation("Creating project {ProjectName}", request.ProjectName);

            // 1. Create folder structure
            projectRoot = CreateStructure(request);

            // 2. Build domain object
            var project = BuildProject(request, projectRoot);

            // 3. Persist project.json
            PersistProject(project);

            // 4. Set current project
            await _projectService.CreateProjectAsync(project, cancellationToken);

            _logger.LogInformation("Project {ProjectName} created successfully", project.Name);
        }
        catch (Exception ex)
        {
            Rollback(projectRoot, ex);
            throw;
        }
    }

    // =========================
    // PRIVATE METHODS
    // =========================

    private string CreateStructure(CreateProjectRequest request)
    {
        var path = _projectDirectoryService.CreateProjectStructure(
            request.FolderPath,
            request.ProjectName,
            request.ImportFile);

        _logger.LogDebug("Project folder created at {Path}", path);

        return path;
    }

    private static Project BuildProject(CreateProjectRequest request, string root)
    {
        return new Project
        {
            Name = request.ProjectName,
            RootPath = root,
            DBFileName = request.ImportFile,
            Concrete = request.Concrete,
            Steel = request.Steel,
            // ModelFilePath = "model.json" // default
        };
    }

    private void PersistProject(Project project)
    {
        _projectPersistenceService.Save(project);

        _logger.LogDebug("project.json saved at {Path}", project.RootPath);
    }

    private void Rollback(string? projectRoot, Exception ex)
    {
        _logger.LogError(ex, "Failed to create project");

        if (string.IsNullOrWhiteSpace(projectRoot))
            return;

        if (!Directory.Exists(projectRoot))
            return;

        try
        {
            Directory.Delete(projectRoot, recursive: true);

            _logger.LogWarning("Rollback completed. Deleted folder {Path}", projectRoot);
        }
        catch (Exception rollbackEx)
        {
            _logger.LogWarning(rollbackEx,
                "Rollback failed for project folder {Path}", projectRoot);
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