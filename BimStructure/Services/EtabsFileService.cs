using System.IO;
using Microsoft.Extensions.Logging;

namespace BimStructure.Services;

public sealed class EtabsFileService : IEtabsFileService
{
    private readonly ILogger<EtabsFileService> _logger;

    public EtabsFileService(ILogger<EtabsFileService> logger)
    {
        _logger = logger;
    }

    public string CopyDatabase(string sourcePath, string projectFolder)
    {
        var etabsFolder = Path.Combine(projectFolder, "etabs");

        Directory.CreateDirectory(etabsFolder);

        var destination = Path.Combine(etabsFolder, "output.accdb");

        File.Copy(sourcePath, destination, overwrite: true);

        return destination;
    }
    
}