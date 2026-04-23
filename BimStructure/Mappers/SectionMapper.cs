using System.Data;
using BimStructure.Dtos;
using BimStructure.Utils;

namespace BimStructure.Mappers;

public static class SectionMapper
{
    public static SectionDto Map(IDataRecord record)
    {
        return new SectionDto
        {
            Name = record.GetRequiredString("Name"),
            Depth = record.GetDoubleOrDefault("Depth"),
            Width = record.GetDoubleOrDefault("Width"),
        };
    }
}