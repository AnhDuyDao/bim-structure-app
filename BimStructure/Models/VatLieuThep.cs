namespace BimStructure.Models;

public class VatLieuThep
{
    [System.ComponentModel.DisplayName("Nhóm cốt thép")]
    public string Ten { get; set; }
    [System.ComponentModel.DisplayName("Cường độ chịu kéo \nthép dọc Rs (MPa)")]
    public double Rs { get; set; }

    [System.ComponentModel.DisplayName("Cường độ chịu kéo \nthép ngang Rsw (MPa)")]
    public double Rsw { get; set; }

    [System.ComponentModel.DisplayName("Cường độ chịu nén \nthép dọc Rsc (MPa)")]
    public double Rsc { get; set; }
}