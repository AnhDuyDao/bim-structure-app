using System.Data;
using BimStructure.Dtos;
using BimStructure.Utils;

namespace BimStructure.Mappers;

public static class GridLineMapper
{
    public static GridLineDto Map(IDataRecord record)
    {
        return new GridLineDto
        {
            Id = record.GetRequiredString("ID"),
            GridLineType = record.GetRequiredString("Grid Line Type"),
            Ordinate = record.GetDoubleOrDefault("Ordinate")
        };
    }
}
