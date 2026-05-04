using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using BimStructure.Services;
using Nice3point.Revit.Toolkit.External;

namespace BimStructure.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class SaveProjectCommand : ExternalCommand
{
    public override void Execute()
    {
        var doc = UiDocument.Document;

        // 🔥 Resolve services từ DI
        var revitService = Host.GetService<IRevitDocumentService>();
        var projectService = Host.GetService<IProjectService>();

        var project = projectService.CurrentProject;

        if (project == null)
        {
            TaskDialog.Show("BimStructure", "No project is currently loaded.");
            return;
        }

        try
        {
            revitService.Save(doc, project);

            TaskDialog.Show("BimStructure", "Project saved successfully!");
        }
        catch (Exception ex)
        {
            TaskDialog.Show("BimStructure", $"Save failed:\n{ex.Message}");
        }
    }
}