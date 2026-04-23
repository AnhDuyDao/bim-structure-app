using System.Data;
using BimStructure.Dtos;
using BimStructure.Utils;

namespace BimStructure.Mappers;

public static class StoryDefinitionMapper
{
    public static StoryDefinitionDto Map(IDataRecord record)
    {
        return new StoryDefinitionDto
        {
            Name = record.GetRequiredString("Name"),
            Height = record.GetDoubleOrDefault("Height")
        };
    }
}
