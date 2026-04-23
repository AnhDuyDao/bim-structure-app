using BimStructure.Mappers;
using BimStructure.Models;
using BimStructure.Repositories;

namespace BimStructure.Services;

public class FrameService : IFrameService
{
    private readonly IFrameRepository _frameRepository;
    private readonly IStoryService _storyService;
    private readonly ISectionRepository _sectionRepository;
    private readonly IPointBaysRepository _pointRepository;
    private readonly IColumnBaysRepository _columnRepo;
    private readonly IBeamBaysRepository _beamRepo;

    public FrameService(IFrameRepository frameRepository, IStoryService storyService, ISectionRepository sectionRepository, IPointBaysRepository pointRepository, IColumnBaysRepository columnRepo, IBeamBaysRepository beamRepo)
    {
        _frameRepository = frameRepository;
        _storyService = storyService;
        _sectionRepository = sectionRepository;
        _pointRepository = pointRepository;
        _columnRepo = columnRepo;
        _beamRepo = beamRepo;
    }

    public async Task<IReadOnlyList<DBFrame>> GetFramesAsync(
        string databasePath,
        CancellationToken cancellationToken = default)
    {
        // chạy song song để tối ưu performance
        var frameTask = _frameRepository.GetFramesAsync(databasePath);
        var storyTask = _storyService.GetAllStoriesAsync(databasePath);
        var sectionTask = _sectionRepository.GetSectionsAsync(databasePath);
        var pointTask = _pointRepository.GetPointBaysAsync(databasePath);
        var columnTask = _columnRepo.GetColumnBaysAsync(databasePath);
        var beamTask = _beamRepo.GetBeamBaysAsync(databasePath);

        await Task.WhenAll(frameTask, storyTask, sectionTask, pointTask, columnTask, beamTask);

        var stories = storyTask.Result.ToDictionary(x => x.Name);
        var sections = sectionTask.Result.ToDictionary(
            x => x.Name,
            x => new DBSection
            {
                Name = x.Name,
                Depth = x.Depth,
                Width = x.Width
            });

        var points = pointTask.Result.ToDictionary(
            x => x.Label,
            x => new DBPoint
            {
                X = x.X,
                Y = x.Y
            });

        var columnConnections = columnTask.Result.ToDictionary(
            x => x.Label,
            x => new[] { x.PointBayI, x.PointBayJ });

        var beamConnections = beamTask.Result.ToDictionary(
            x => x.Label,
            x => new[] { x.PointBayI, x.PointBayJ });

        // mapping tách riêng
        var frames = frameTask.Result
            .Select(dto => FrameMapper.ToModel(
                dto,
                stories,
                sections,
                points,
                columnConnections,
                beamConnections))
            .ToList();

        return frames;
    }
}