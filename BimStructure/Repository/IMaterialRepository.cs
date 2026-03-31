using BimStructure.Models;

namespace BimStructure.Repository;

public interface IMaterialRepository
{
    List<ConcreteMaterial> GetConcreteMaterials();
    List<SteelMaterial> GetSteelMaterials();
}