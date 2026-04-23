using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface IUnitRepository
{
    ProgramControlDto GetProgramControl(string databasePath);
}
