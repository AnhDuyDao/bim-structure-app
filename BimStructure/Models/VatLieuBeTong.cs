namespace BimStructure.Models;

public class VatLieuBeTong
{
    [System.ComponentModel.DisplayName("Vật liệu bê tông")]
    public string Ten { get; set; } = string.Empty;

    [System.ComponentModel.DisplayName("Cấp độ bền chịu nén \ndọc trục Rb (MPa)")]
    public double Rb { get; set; }

    [System.ComponentModel.DisplayName("Cấp độ bền chịu kéo \ndọc trục Rbt (MPa)")]
    public double Rbt { get; set; }

    [System.ComponentModel.DisplayName("Mô đun đàn hồi \nEb x E-3 (MPa)")]
    public double Eb { get; set; }
}
