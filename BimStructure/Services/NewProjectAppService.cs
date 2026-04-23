using System;
using BimStructure.Models;

namespace BimStructure.Services;

public sealed class NewProjectAppService : INewProjectAppService
{
    private readonly IUnitService _unitService;
    private readonly IProjectService _projectService;

    public NewProjectAppService(
        IUnitService unitService,
        IProjectService projectService)
    {
        _unitService = unitService;
        _projectService = projectService;
    }

    public DBUnitSet ReadUnits(string accessFilePath)
    {
        return _unitService.GetUnits(accessFilePath);
    }

    public void CreateProject(CreateProjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ProjectName))
        {
            throw new ArgumentException("Project name is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.FolderPath))
        {
            throw new ArgumentException("Folder path is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.ImportFile))
        {
            throw new ArgumentException("Import file is required.", nameof(request));
        }

        var project = new Project
        {
            Name = request.ProjectName,
            RootPath = request.FolderPath,
            DBFileName = request.ImportFile,
            Concrete = request.Concrete,
            Steel = request.Steel
        };

        _projectService.CreateProject(project);
    }
}
