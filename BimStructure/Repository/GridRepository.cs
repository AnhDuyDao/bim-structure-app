using System.Data;
using System.Linq;
using BimStructure.Models;

namespace BimStructure.Repository;

public sealed class GridRepository : IGridRepository
{
    private const string GridDefinitionsQuery = "SELECT * FROM [Grid Definitions - Grid Lines]";

    private readonly IAccessRepository _accessRepository;

    public GridRepository(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public Dictionary<string, DBGrid> GetGrids(string databasePath)
    {
        var table = _accessRepository.GetData(databasePath, GridDefinitionsQuery);
        return table.Rows.Cast<DataRow>().ToDictionary(
            row => GetString(row, "ID"),
            row => new DBGrid
            {
                Name = GetString(row, "ID"),
                Direction = GetString(row, "Grid Line Type") == "X (Cartesian)"
                    ? GridDirection.X
                    : GridDirection.Y,
                Coordinate = GetDouble(row, "Ordinate")
            });
    }

    private static string GetString(DataRow row, string columnName)
    {
        return row[columnName]?.ToString() ?? string.Empty;
    }

    private static double GetDouble(DataRow row, string columnName)
    {
        return row[columnName] is DBNull ? 0d : Convert.ToDouble(row[columnName]);
    }
}
