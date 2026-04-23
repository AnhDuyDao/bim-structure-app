using System.Data;
using BimStructure.Dtos;
using BimStructure.Utils;

namespace BimStructure.Mappers;

public static class ProgramControlMapper
{
    public static ProgramControlDto Map(IDataRecord record)
    {
        return new ProgramControlDto
        {
            CurrentUnits = record.GetRequiredString("CurrUnits")
        };
    }
}
