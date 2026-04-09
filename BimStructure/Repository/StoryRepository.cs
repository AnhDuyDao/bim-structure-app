using BimStructure.Models;
using System.Data;

namespace BimStructure.Repository;

public class StoryRepository : IStoryRepository
{
    private const string StoriesDefinitionsQuery = "SELECT * FROM [Story Definitions]";

    private readonly IAccessRepository _accessRepository;

    public StoryRepository(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public List<DBStory> GetStories(string databasePath)
    {
        var table = _accessRepository.GetData(databasePath, StoriesDefinitionsQuery);
        return table.Rows.Cast<DataRow>()
            .Select(row => new DBStory
            {
                Name = GetString(row,"Name"),
                Height = GetDouble(row,"Height"),
            })
            .ToList(); 
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