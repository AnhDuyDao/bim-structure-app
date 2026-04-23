using System;
using System.Linq;
using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public DBUnitSet GetUnits(string databasePath)
    {
        var programControl = _unitRepository.GetProgramControl(databasePath);
        return ParseUnits(programControl.CurrentUnits);
    }

    private static DBUnitSet ParseUnits(string currUnits)
    {
        var parts = currUnits
            .Split(',')
            .Select(x => x.Trim())
            .ToArray();

        if (parts.Length < 2)
        {
            throw new InvalidOperationException($"Gia tri CurrUnits khong hop le: '{currUnits}'.");
        }

        var forceUnit = ParseForceUnit(parts[0]);
        var lengthUnit = ParseLengthUnit(parts[1]);

        return new DBUnitSet
        {
            ForceUnit = forceUnit,
            LengthUnit = lengthUnit,
            MomentFUnit = GetMomentUnit(forceUnit, lengthUnit)
        };
    }

    private static UnitType ParseForceUnit(string value)
    {
        return value switch
        {
            "N" => UnitType.NEWTON,
            "daN" => UnitType.DECANEWTON,
            "kN" => UnitType.KILONEWTON,
            "kgF" => UnitType.KILOGRAM_FORCE,
            "Ton" or "TonF" => UnitType.TON_FORCE,
            _ => throw new NotSupportedException($"Chua ho tro don vi luc '{value}'.")
        };
    }

    private static UnitType ParseLengthUnit(string value)
    {
        return value switch
        {
            "mm" => UnitType.MILLIMETER,
            "cm" => UnitType.CENTIMETER,
            "m" => UnitType.METER,
            _ => throw new NotSupportedException($"Chua ho tro don vi chieu dai '{value}'.")
        };
    }

    private static UnitType GetMomentUnit(UnitType forceUnit, UnitType lengthUnit)
    {
        return (forceUnit, lengthUnit) switch
        {
            (UnitType.NEWTON, UnitType.MILLIMETER) => UnitType.NEWTON_MILLIMETER,
            (UnitType.NEWTON, UnitType.METER) => UnitType.NEWTON_METER,
            (UnitType.KILONEWTON, UnitType.METER) => UnitType.KILONEWTON_METER,
            (UnitType.TON_FORCE, UnitType.METER) => UnitType.TONF_METER,
            _ => throw new NotSupportedException(
                $"Chua ho tro don vi moment tu cap '{forceUnit}' va '{lengthUnit}'.")
        };
    }
}
