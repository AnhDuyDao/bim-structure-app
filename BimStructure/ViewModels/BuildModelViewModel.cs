using System.Collections.ObjectModel;
using BimStructure.Models;
using BimStructure.Services;

namespace BimStructure.ViewModels;

public sealed partial class BuildModelViewModel : ObservableObject
{
    private readonly IGridService _gridService;
    private readonly IProjectService _projectService;
    private readonly IStoryService _storyService;
    private readonly IFrameService _frameService;

    public ObservableCollection<DBGrid> GridX { get; } = new();
    public ObservableCollection<DBGrid> GridY { get; } = new();
    public ObservableCollection<DBStory> GridHeight { get; } = new();
    public ObservableCollection<DBFrame> GridDetailsBeam { get; } = new();
    public ObservableCollection<DBFrame> GridDetailsColumn { get; } = new();

    public BuildModelViewModel(
        IGridService gridService,
        IProjectService projectService,
        IStoryService storyService, IFrameService frameService)
    {
        _gridService = gridService;
        _projectService = projectService;
        _storyService = storyService;
        _frameService = frameService;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var project = _projectService.CurrentProject;

        if (project == null)
            return;

        var databasePath = project.DBFileName;

        try
        {
            var grids = await _gridService.GetGridsAsync(databasePath);
            var stories = await _storyService.GetAllStoriesAsync(databasePath);
            var frames = await _frameService.GetFramesAsync(databasePath);

            UpdateGrids(grids);
            UpdateStories(stories);
            UpdateFrames(frames);
        }
        catch (Exception ex)
        {
            // TODO: inject ILogger hoặc IMessageService
            Console.WriteLine(ex);
        }
    }

    private void UpdateGrids(IReadOnlyDictionary<string, DBGrid> grids)
    {
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

    private void UpdateStories(IReadOnlyList<DBStory> stories)
    {
        GridHeight.Clear();

        foreach (var story in stories)
        {
            GridHeight.Add(story);
        }
    }
    
    private void UpdateFrames(IReadOnlyList<DBFrame> frames)
    {
        GridDetailsBeam.Clear();
        GridDetailsColumn.Clear();

        foreach (var frame in frames)
        {
            if (frame.Type == FrameType.Beam)
                GridDetailsBeam.Add(frame);
            else
                GridDetailsColumn.Add(frame);
        }
    }
}