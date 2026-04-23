namespace BimStructure.Models;

public class DBFrame
{
    public string Name { get; set; } = string.Empty;

    public FrameType Type { get; set; }

    public DBPoint IEnd { get; set; } = new();

    public DBPoint JEnd { get; set; } = new();

    public DBStory Story { get; set; } = new();

    public double Length { get; set; }

    public DBSection Section { get; set; } = new();

    public object? GraphicId { get; set; }

    public double a;

    public double Sc;
}

public enum FrameType
{
    Beam,
    Column
}
