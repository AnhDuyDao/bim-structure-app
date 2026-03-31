using BimStructure.Models;

namespace BimStructure.Services;

public interface IMaterialService
{
    List<ConcreteMaterial> GetConcreteMaterials();
    List<SteelMaterial> GetSteelMaterials();
}
