using BimStructure.Models;

namespace BimStructure.Services;

public sealed class CreateProjectRequest
{
    public string ProjectName { get; set; } = string.Empty;

    public string FolderPath { get; set; } = string.Empty;

    public string ImportFile { get; set; } = string.Empty;

    public ConcreteMaterial? Concrete { get; set; }

    public SteelMaterial? Steel { get; set; }
}
