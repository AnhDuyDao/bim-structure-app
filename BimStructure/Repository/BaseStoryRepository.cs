using BimStructure.Models;
using System.Data;
using System.Globalization;

namespace BimStructure.Repository;

public class BaseStoryRepository : IBaseStoryRepository
{
    private const string StoryBaseDefinitionsQuery = "SELECT * FROM [Tower and Base Story Definitions]";

    private readonly IAccessRepository _accessRepository;

    public BaseStoryRepository(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public List<DBStory> GetBaseStory(string databasePath)
    {
        var table = _accessRepository.GetData(databasePath, StoryBaseDefinitionsQuery);
        return table.Rows.Cast<DataRow>()
            .Select(row => new DBStory
            {
                Name = GetString(row,"BSName"),
                Elevation = GetDouble(row, "BSElev")
            })
            .ToList(); 
    }

    private static string GetString(DataRow row, string columnName)
    {
        return row[columnName]?.ToString() ?? string.Empty;
    }

    private static double GetDouble(DataRow row, string columnName)
    {
        return row[columnName] is DBNull ? 0d : Convert.ToDouble(row[columnName], CultureInfo.InvariantCulture);
    }
}