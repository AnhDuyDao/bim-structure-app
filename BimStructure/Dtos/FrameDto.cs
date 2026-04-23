namespace BimStructure.Dtos;

public enum FrameTypeDto {Beam, Column}

public sealed class FrameDto
{
    public string Label { get; set; } = string.Empty;
    public string Story { get; set; } = string.Empty;
    public double Length { get; set; }
    public string Section { get; set; } = string.Empty;
    public string DesignType { get; set; } = string.Empty;
}