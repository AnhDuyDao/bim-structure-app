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
        var newProjectView = Host.GetService<NewProjectView>();
        var dialogResult = newProjectView.ShowDialog();
        if (dialogResult != true)
        {
            return;
        }

        var buildModelView = Host.GetService<BuildModelView>();
        buildModelView.ShowDialog();
    }
}
