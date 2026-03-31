using BimStructure.Models;

namespace BimStructure.Services;

public interface IUnitService
{
    DBUnitSet GetUnits(string databasePath);
}
