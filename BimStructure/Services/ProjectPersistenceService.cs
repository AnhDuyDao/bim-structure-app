using System.IO;
using BimStructure.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BimStructure.Services;

public sealed class ProjectPersistenceService : IProjectPersistenceService
{
    private readonly ILogger<ProjectPersistenceService> _logger;

    public ProjectPersistenceService(ILogger<ProjectPersistenceService> logger)
    {
        _logger = logger;
    }

    public string Save(Project project)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project));

        if (string.IsNullOrWhiteSpace(project.RootPath))
            throw new ArgumentException("Project root path is required");
        
        var filePath = Path.Combine(project.RootPath, "project.json");

        try
        {
            _logger.LogInformation("Saving project to {Path}", filePath);

            var json = JsonConvert.SerializeObject(project, Formatting.Indented);

            File.WriteAllText(filePath, json);

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save project");
            throw;
        }
    }
}
