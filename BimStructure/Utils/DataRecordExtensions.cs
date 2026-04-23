using System.Data;
using System.Globalization;

namespace BimStructure.Utils;

public static class DataRecordExtensions
{
    public static string GetRequiredString(this IDataRecord record, string columnName)
    {
        var value = record[columnName];
        if (value is null || value is DBNull)
        {
            throw new InvalidOperationException($"Column '{columnName}' is required.");
        }

        return value.ToString()!;
    }

    public static double GetDoubleOrDefault(
        this IDataRecord record,
        string columnName,
        double defaultValue = 0d)
    {
        var value = record[columnName];
        if (value is null || value is DBNull)
        {
            return defaultValue;
        }

        return Convert.ToDouble(value, CultureInfo.InvariantCulture);
    }
}
