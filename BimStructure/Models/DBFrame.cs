namespace BimStructure.Models;

public class DBFrame
{
    public string Name { get; set; }
    public FrameType Type { get; set; }
    public DBPoint IEnd { get; set; }
    public DBPoint JEnd { get; set; }
    public DBStory Story { get; set; }
    public double Length { get; set; }
    public DBSection Section { get; set; }
    public object GraphicId { get; set; }


    public double a;
    public double Sc;
}
public enum FrameType
{
    Beam,
    Column
}