using System.Collections.ObjectModel;
using BimStructure.Models;
using BimStructure.Services;

namespace BimStructure.ViewModels;

public sealed class BuildModelViewModel : ObservableObject
{
    private readonly IGridService _gridService;
    private readonly IProjectService _projectService;
    
    public ObservableCollection<DBGrid> GridX { get; set; } = new();
    public ObservableCollection<DBGrid> GridY { get; set; } = new();

    public BuildModelViewModel(IGridService gridService, IProjectService projectService)
    {
        _gridService = gridService;
        _projectService = projectService;
        LoadGrids();
    }
    
    public void LoadGrids()
    {
        var project = _projectService.CurrentProject;

        if (project == null)
            return;

        var databasePath = project.DBFileName; // hoặc xử lý thêm nếu cần
        var grids = _gridService.GetGrids(databasePath);

        GridX.Clear();
        GridY.Clear();

        foreach (var grid in grids.Values)
        {
            if (grid.Direction == GridDirection.X)
                GridX.Add(grid);
            else
                GridY.Add(grid);
        }
    }
}