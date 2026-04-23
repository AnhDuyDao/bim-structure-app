using BimStructure.Models;

namespace BimStructure.Repositories;

public class MaterialRepository : IMaterialRepository
{
    public List<ConcreteMaterial> GetConcreteMaterials()
    {
        return new List<ConcreteMaterial>
        {
            new ConcreteMaterial { Name = "B20", Rb = 11.5, Rbt = 0.9, Eb = 27 },
            new ConcreteMaterial { Name = "B25", Rb = 14.5, Rbt = 1.05, Eb = 30 },
            new ConcreteMaterial { Name = "B30", Rb = 17.0, Rbt = 1.15, Eb = 32.5 }
        };
    }
    
    public List<SteelMaterial> GetSteelMaterials()
    {
        return new List<SteelMaterial>
        {
            new SteelMaterial { Name = "CB240-T", Rs = 210, Rsw = 170, Rsc = 210 },
            new SteelMaterial { Name = "CB300-V", Rs = 260, Rsw = 225, Rsc = 260 },
            new SteelMaterial { Name = "CB400-V", Rs = 350, Rsw = 280, Rsc = 350 }
        };
    }
}