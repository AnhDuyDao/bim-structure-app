using BimStructure.Models;

namespace BimStructure.Services;

public sealed class NewProjectAppService : INewProjectAppService
{
    private readonly IUnitService _unitService;
    private readonly IProjectService _projectService;
    private readonly IProjectDirectoryService _projectDirectoryService;

    public NewProjectAppService(
        IUnitService unitService,
        IProjectService projectService, IProjectDirectoryService projectDirectoryService)
    {
        _unitService = unitService;
        _projectService = projectService;
        _projectDirectoryService = projectDirectoryService;
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
        
        var projectRoot = _projectDirectoryService.CreateProjectStructure(
            request.FolderPath,
            request.ProjectName,
            request.ImportFile);
        
        var project = new Project
        {
            Name = request.ProjectName,
            RootPath = projectRoot,
            DBFileName = request.ImportFile,
            Concrete = request.Concrete,
            Steel = request.Steel
        };

        await _projectService.CreateProjectAsync(project, cancellationToken);
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