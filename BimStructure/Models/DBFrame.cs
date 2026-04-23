namespace BimStructure.Models;

public class DBFrame
{
    public string Name { get; set; } = string.Empty;
    public FrameType Type { get; set; }

    public DBPoint IEnd { get; set; } = null!;
    public DBPoint JEnd { get; set; } = null!;
    public DBStory Story { get; set; } = null!;

    public double Length { get; set; }
    public DBSection Section { get; set; } = null!;

    public double A { get; set; }
    public double Sc { get; set; }
}

public enum FrameType
{
    Beam,
    Column
}
