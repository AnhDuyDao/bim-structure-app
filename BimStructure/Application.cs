using Nice3point.Revit.Toolkit.External;
using BimStructure.Commands;

namespace BimStructure;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        Host.Start();
        CreateRibbon();
    }

    private void CreateRibbon()
    {
        // var panel = Application.CreatePanel("Dự Án","BimStructure");

        Application.CreateRibbonTab("BimStructure");
        
        var panelDuAn = Application.CreateRibbonPanel("BimStructure","Dự Án");

        panelDuAn.AddPushButton<DuAnMoiCommand>("Dự án mới")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-new-project-16.png");
        
        panelDuAn.AddPushButton<HelloCommand>("Mở dự án")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-project-manager-16.png");
        
        panelDuAn.AddPushButton<LuuDuAnCommand>("Lưu dự án")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-bookmark-16.png");
        
        var panelTinhToan = Application.CreateRibbonPanel("BimStructure","Tính toán");
        
        panelTinhToan.AddPushButton<TinhToanCommand>("Mở dự án")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-edit-16.png");
        
        var panelTheHienBanVe =  Application.CreateRibbonPanel("BimStructure", "Thể hiện bản vẽ");
        
        panelTheHienBanVe.AddPushButton<HelloCommand>("Tạo mặt cắt")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-selection-loading-16.png");
        
        panelTheHienBanVe.AddPushButton<StartupCommand>("Tạo bản vẽ")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-home-16.png");
        
        var panelThongTinDuAn =  Application.CreateRibbonPanel("BimStructure", "Thông tin dự án");
        
        panelThongTinDuAn.AddPushButton<HelloCommand>("Tạo bản vẽ")
            .SetLargeImage("/BimStructure;component/Resources/Icons/icons8-about-16.png");
    }
}