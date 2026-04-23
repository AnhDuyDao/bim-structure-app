using BimStructure.Models;

namespace BimStructure.Services;

public interface INewProjectAppService
{
    DBUnitSet ReadUnits(string accessFilePath);

    void CreateProject(CreateProjectRequest request);
}
