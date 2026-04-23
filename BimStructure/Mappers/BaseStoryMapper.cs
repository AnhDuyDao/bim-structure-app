using System.Data;
using BimStructure.Dtos;
using BimStructure.Utils;

namespace BimStructure.Mappers;

public static class BaseStoryMapper
{
    public static BaseStoryDto Map(IDataRecord record)
    {
        return new BaseStoryDto
        {
            Name = record.GetRequiredString("BSName"),
            Elevation = record.GetDoubleOrDefault("BSElev")
        };
    }
}
