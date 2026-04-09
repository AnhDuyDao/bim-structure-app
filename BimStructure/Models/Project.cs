namespace BimStructure.Models;

public class Project
{
    public string Name { get; set; } = string.Empty;

    public string RootPath { get; set; } = string.Empty;

    public string DBFileName { get; set; } = string.Empty;

    public DBModel? Model { get; set; }

    public SteelMaterial? Steel { get; set; }

    public ConcreteMaterial? Concrete { get; set; }
    
}