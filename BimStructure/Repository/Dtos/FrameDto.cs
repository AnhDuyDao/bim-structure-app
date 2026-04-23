namespace BimStructure.Repository.Dtos;

public enum FrameTypeDto {Beam, Column}

public sealed class FrameDto
{
    public string Label { get; set; } = string.Empty;
    public FrameTypeDto Type { get; set; }
    public string Story { get; set; }
    public double Length { get; set; }
    public string Section { get; set; }
    public string IEndId { get; set; }
    public string JEndId { get; set; }
}