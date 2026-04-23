using System.Data;
using BimStructure.Repository.Dtos;
using BimStructure.Utils;

namespace BimStructure.Repository.Mappers;

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