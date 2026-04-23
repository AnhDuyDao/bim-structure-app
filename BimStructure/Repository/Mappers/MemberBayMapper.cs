using System.Data;
using BimStructure.Repository.Dtos;
using BimStructure.Utils;

namespace BimStructure.Repository.Mappers;

public static class MemberBayMapper
{
    public static MemberBayDto Map(IDataRecord record)
    {
        return new MemberBayDto
        {
            Label = record.GetRequiredString("Label"),
            PointBayI = record.GetRequiredString("PointBayI"),
            PointBayJ =  record.GetRequiredString("PointBayJ")
        };
    }
}