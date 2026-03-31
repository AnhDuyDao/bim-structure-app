using System;

namespace BimStructure.Repository;

public sealed class UnitRepository : IUnitRepository
{
    private const string ProgramControlQuery = "SELECT [CurrUnits] FROM [Program Control];";

    private readonly IAccessRepository _accessRepository;

    public UnitRepository(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public string GetCurrentUnits(string databasePath)
    {
        var table = _accessRepository.GetData(databasePath, ProgramControlQuery);

        if (table.Rows.Count == 0)
        {
            throw new InvalidOperationException("Khong tim thay du lieu trong bang [Program Control].");
        }

        var currUnits = table.Rows[0]["CurrUnits"]?.ToString();
        if (string.IsNullOrWhiteSpace(currUnits))
        {
            throw new InvalidOperationException("Khong doc duoc gia tri [CurrUnits].");
        }

        return currUnits;
    }
}
