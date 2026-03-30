using BimStructure.Models;

namespace BimStructure.Repository;

public class VatLieuRepository : IVatLieuRepository
{
    public List<VatLieuBeTong> GetBeTong()
    {
        return new List<VatLieuBeTong>
        {
            new VatLieuBeTong { Ten = "B20", Rb = 11.5, Rbt = 0.9, Eb = 27 },
            new VatLieuBeTong { Ten = "B25", Rb = 14.5, Rbt = 1.05, Eb = 30 },
            new VatLieuBeTong { Ten = "B30", Rb = 17.0, Rbt = 1.15, Eb = 32.5 }
        };
    }
    
    public List<VatLieuThep> GetThep()
    {
        return new List<VatLieuThep>
        {
            new VatLieuThep { Ten = "CB240-T", Rs = 210, Rsw = 170, Rsc = 210 },
            new VatLieuThep { Ten = "CB300-V", Rs = 260, Rsw = 225, Rsc = 260 },
            new VatLieuThep { Ten = "CB400-V", Rs = 350, Rsw = 280, Rsc = 350 }
        };
    }
}