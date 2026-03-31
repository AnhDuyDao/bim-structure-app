namespace BimStructure.Models;

public class SteelMaterial
{
    [System.ComponentModel.DisplayName("Nhóm cốt thép")]
    public string Name { get; set; } = String.Empty;
    [System.ComponentModel.DisplayName("Cường độ chịu kéo \nthép dọc Rs (MPa)")]
    public double Rs { get; set; }

    [System.ComponentModel.DisplayName("Cường độ chịu kéo \nthép ngang Rsw (MPa)")]
    public double Rsw { get; set; }

    [System.ComponentModel.DisplayName("Cường độ chịu nén \nthép dọc Rsc (MPa)")]
    public double Rsc { get; set; }
}