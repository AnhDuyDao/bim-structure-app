namespace BimStructure.Models;

public class DBGrid
{
    public string Name { get; set; }
    public GridDirection Direction { get; set; }
    public double Coordinate { get; set; }
}

public enum GridDirection
{
    X,
    Y
}
