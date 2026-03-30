using BimStructure.Models;

namespace BimStructure.Repository;

public interface IVatLieuRepository
{
    List<VatLieuBeTong> GetBeTong();
    List<VatLieuThep> GetThep();
}