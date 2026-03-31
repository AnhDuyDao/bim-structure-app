namespace BimStructure.Models;

public class DBUnitSet
{
    public UnitType LengthUnit { get; set; }
    public UnitType ForceUnit { get; set; }
    public UnitType MomentFUnit { get; set; }
}

public enum UnitType
{
    NEWTON,
    DECANEWTON,
    KILONEWTON,
    KILOGRAM_FORCE,
    TON_FORCE,

    MILLIMETER,
    CENTIMETER,
    METER,

    SQUARE_MILLIMETER,
    SQUARE_CENTIMETER,
    SQUARE_METER,

    NEWTON_PER_SQUARE_METER,
    NEWTON_PER_SQUARE_MILLIMETER,

    NEWTON_MILLIMETER,
    NEWTON_METER,
    TONF_METER,
    KILONEWTON_METER,
}