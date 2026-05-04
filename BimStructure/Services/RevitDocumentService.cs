using System.IO;
using BimStructure.Models;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class RevitDocumentService : IRevitDocumentService
{
    private readonly ILogger<RevitDocumentService> _logger;

    public RevitDocumentService(ILogger<RevitDocumentService> logger)
    {
        _logger = logger;
    }

    public void Save(Document document, Project project)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        if (string.IsNullOrWhiteSpace(project.RootPath))
            throw new ArgumentException("Project folder is required");

        if (string.IsNullOrWhiteSpace(project.Name))
            throw new ArgumentException("Project name is required");
        
        var filePath = Path.Combine(project.RootPath, $"{project.Name}.rvt");

        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogInformation("Saving new Revit file: {Path}", filePath);

                document.SaveAs(filePath);
            }
            else
            {
                _logger.LogInformation("Saving existing Revit file");

                document.Save();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save Revit document");
            throw;
        }
    }
}