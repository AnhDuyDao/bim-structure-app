using BimStructure.Models;
using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class VatLieuService : IVatLieuService
{
    private readonly IVatLieuRepository _vatLieuRepository;

    public VatLieuService(IVatLieuRepository vatLieuRepository)
    {
        _vatLieuRepository = vatLieuRepository;
    }

    public List<VatLieuBeTong> GetBeTong()
    {
        return _vatLieuRepository.GetBeTong();
    }

    public List<VatLieuThep> GetThep()
    {
        return _vatLieuRepository.GetThep();
    }
}
