using System.Data;
using BimStructure.Repository.Dtos;
using BimStructure.Utils;

namespace BimStructure.Repository.Mappers;

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
