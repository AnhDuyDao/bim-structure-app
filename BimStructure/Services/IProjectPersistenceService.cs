using BimStructure.Models;

namespace BimStructure.Services;

public interface IProjectPersistenceService
{
    string Save(Project project);
}