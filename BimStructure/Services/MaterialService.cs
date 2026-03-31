using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public List<ConcreteMaterial> GetConcreteMaterials()
    {
        return _materialRepository.GetConcreteMaterials();
    }

    public List<SteelMaterial> GetSteelMaterials()
    {
        return _materialRepository.GetSteelMaterials();
    }
}
