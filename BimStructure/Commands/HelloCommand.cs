using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using BimStructure.Views;

namespace BimStructure.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class HelloCommand : ExternalCommand
{
    public override void Execute()
    {
        var view = Host.GetService<DuAnMoiView>();
        view.ShowDialog();
    }
}