namespace BimStructure.Models;

public class DBModel
{
    public DBUnitSet Units { get; set; }

    public Dictionary<string, DBGrid> GridSet { get; set; }
    public Dictionary<string, DBStory> LevelSet { get; set; }

    public IEnumerable<DBFrame> FrameSet { get; set; }
}