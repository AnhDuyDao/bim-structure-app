using System.Data;
using BimStructure.Dtos;
using BimStructure.Models;

namespace BimStructure.Mappers;

public static class FrameMapper
{
    public static FrameDto Map(IDataRecord record)
    {
        return new FrameDto
        {
            Label = record.GetString(record.GetOrdinal("Label")),
            Story = record.GetString(record.GetOrdinal("Story")),
            Length = record.GetDouble(record.GetOrdinal("Length")),
            Section = record.GetString(record.GetOrdinal("Analysis Section")),
            DesignType = record.GetString(record.GetOrdinal("Design Type"))
        };
    }
    
    public static DBFrame ToModel(
        FrameDto dto,
        IReadOnlyDictionary<string, DBStory> stories,
        IReadOnlyDictionary<string, DBSection> sections,
        IReadOnlyDictionary<string, DBPoint> points,
        IReadOnlyDictionary<string, string[]> columnConnections,
        IReadOnlyDictionary<string, string[]> beamConnections)
    {
        if (!stories.TryGetValue(dto.Story, out var story))
            throw new KeyNotFoundException($"Story not found: {dto.Story}");

        if (!sections.TryGetValue(dto.Section, out var section))
            throw new KeyNotFoundException($"Section not found: {dto.Section}");

        var isColumn = dto.DesignType == "Column";

        var connections = isColumn ? columnConnections : beamConnections;

        if (!connections.TryGetValue(dto.Label, out var lineEnd))
            throw new KeyNotFoundException($"Connection not found: {dto.Label}");

        if (!points.TryGetValue(lineEnd[0], out var iEnd))
            throw new KeyNotFoundException($"Point not found: {lineEnd[0]}");

        if (!points.TryGetValue(lineEnd[1], out var jEnd))
            throw new KeyNotFoundException($"Point not found: {lineEnd[1]}");

        return new DBFrame
        {
            Name = dto.Label,
            Story = story,
            Section = section,
            Length = dto.Length,
            Type = isColumn ? FrameType.Column : FrameType.Beam,
            IEnd = iEnd,
            JEnd = jEnd
        };
    }
}