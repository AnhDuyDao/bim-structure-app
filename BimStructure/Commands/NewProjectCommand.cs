using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using BimStructure.Views;

namespace BimStructure.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class NewProjectCommand : ExternalCommand
{
    public override void Execute()
    {
        var view = Host.GetService<NewProjectView>();
        view.ShowDialog();
    }
}