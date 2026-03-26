using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using BimStructure.Views;

namespace BimStructure.Commands;

/// <summary>
///     External command entry point.
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class StartupCommand : ExternalCommand
{
    public override void Execute()
    {
        var view = Host.GetService<BimStructureView>();
        view.ShowDialog();
    }
}