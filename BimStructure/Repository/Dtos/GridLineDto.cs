namespace BimStructure.Repository.Dtos;

public sealed class GridLineDto
{
    public string Id { get; set; } = string.Empty;

    public string GridLineType { get; set; } = string.Empty;

    public double Ordinate { get; set; }
}
