using System.Collections.Generic;
using BimStructure.Models;

namespace BimStructure.Utils;

public static class UnitUtils
{
    public static readonly IReadOnlyDictionary<UnitType, string> StringName =
        new Dictionary<UnitType, string>
        {
            { UnitType.NEWTON, "N" },
            { UnitType.DECANEWTON, "daN" },
            { UnitType.KILONEWTON, "kN" },
            { UnitType.KILOGRAM_FORCE, "kgF" },
            { UnitType.TON_FORCE, "TonF" },

            { UnitType.METER, "m" },
            { UnitType.MILLIMETER, "mm" },
            { UnitType.CENTIMETER, "cm" },

            { UnitType.SQUARE_METER, "m2" },
            { UnitType.SQUARE_MILLIMETER, "mm2" },
            { UnitType.SQUARE_CENTIMETER, "cm2" },

            { UnitType.NEWTON_PER_SQUARE_METER, "N/m2" },
            { UnitType.NEWTON_PER_SQUARE_MILLIMETER, "N/mm2" },

            { UnitType.NEWTON_MILLIMETER, "N.mm" },
            { UnitType.NEWTON_METER, "N.m" },
            { UnitType.TONF_METER, "T.m" },
            { UnitType.KILONEWTON_METER, "kN.m" }
        };

    public static string ToDisplayString(UnitType unitType)
    {
        return StringName.TryGetValue(unitType, out var value)
            ? value
            : unitType.ToString();
    }
}
