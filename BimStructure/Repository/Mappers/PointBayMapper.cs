using System.Data;

using BimStructure.Repository.Dtos;
using BimStructure.Utils;

namespace BimStructure.Repository.Mappers;

public static class PointBayMapper
{
    public static PointBayDto Map(IDataRecord record)
    {
        return new PointBayDto
        {
            Label = record.GetRequiredString("Label"),
            X = record.GetDoubleOrDefault("X"),
            Y = record.GetDoubleOrDefault("Y")
        };
    }
}