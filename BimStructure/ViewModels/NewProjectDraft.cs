using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using BimStructure.Models;

namespace BimStructure.ViewModels;

public sealed partial class NewProjectDraft : ObservableObject
{
    [ObservableProperty]
    private string _projectName = "Du_an_1";

    [ObservableProperty]
    private string _folderPath = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImportFileName))]
    private string _importFile = string.Empty;

    [ObservableProperty]
    private string _lengthUnit = string.Empty;

    [ObservableProperty]
    private string _forceUnit = string.Empty;

    [ObservableProperty]
    private ConcreteMaterial? _selectedConcreteMaterial;

    [ObservableProperty]
    private SteelMaterial? _selectedSteelMaterial;

    public string ImportFileName => Path.GetFileName(ImportFile);

    public bool HasRequiredValues =>
        !string.IsNullOrWhiteSpace(ProjectName) &&
        !string.IsNullOrWhiteSpace(FolderPath) &&
        !string.IsNullOrWhiteSpace(ImportFile) &&
        SelectedConcreteMaterial is not null &&
        SelectedSteelMaterial is not null;
}
