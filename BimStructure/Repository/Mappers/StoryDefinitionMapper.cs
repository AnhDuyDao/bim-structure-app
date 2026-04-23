using System.Data;
using BimStructure.Repository.Dtos;

namespace BimStructure.Repository.Mappers;

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
