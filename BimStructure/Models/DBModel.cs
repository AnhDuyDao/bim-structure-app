namespace BimStructure.Models;

public class DBModel
{
    public DBUnitSet Units { get; set; }

    public Dictionary<string, DBGrid> GridSet { get; set; }
    public Dictionary<string, DBStory> LevelSet { get; set; }

    public IEnumerable<DBFrame> FrameSet { get; set; }
    public DBModel() { }

    public IEnumerable<DBFrame> FrameSetByGrid(DBGrid grid)
    {
        switch (grid.Direction)
        {
            case GridDirection.X:
                return FrameSet.Where(x => x.IEnd.X == grid.Coordinate && x.JEnd.X == grid.Coordinate);
            case GridDirection.Y:

                return FrameSet.Where(x => x.IEnd.Y == grid.Coordinate && x.JEnd.Y == grid.Coordinate);
        }

        return null;
    }
    public DBFrame FrameSetByName(Autodesk.Revit.DB.Element ele)
    {
        return FrameSet.Where(x => x.Name == ele.LookupParameter("Tên").AsString().Split('-')[0] && x.Story.Name == ele.LookupParameter("Tên").AsString().Split('-')[1]).First();
    }
}