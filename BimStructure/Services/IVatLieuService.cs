using BimStructure.Models;

namespace BimStructure.Services;

public interface IVatLieuService
{
    List<VatLieuBeTong> GetBeTong();
    List<VatLieuThep> GetThep();
}
