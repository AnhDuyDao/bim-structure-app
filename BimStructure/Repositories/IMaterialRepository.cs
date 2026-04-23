using BimStructure.Models;

namespace BimStructure.Repositories;

public interface IMaterialRepository
{
    List<ConcreteMaterial> GetConcreteMaterials();
    List<SteelMaterial> GetSteelMaterials();
}