namespace BimStructure.Services;

public interface IEtabsFileService
{
    string CopyDatabase(string sourcePath, string projectFolder);
}