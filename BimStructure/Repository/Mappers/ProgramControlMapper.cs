using System.Data;
using BimStructure.Repository.Dtos;

namespace BimStructure.Repository.Mappers;

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
