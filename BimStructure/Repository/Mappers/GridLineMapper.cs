using System.Data;
using BimStructure.Repository.Dtos;

namespace BimStructure.Repository.Mappers;

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
