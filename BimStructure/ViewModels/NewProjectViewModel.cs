using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BimStructure.Models;
using BimStructure.Services;
using UnitUtils = BimStructure.Utils.UnitUtils;

namespace BimStructure.ViewModels;

public sealed partial class NewProjectViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IMaterialService _materialService;
    private readonly INewProjectAppService _newProjectAppService;
    private readonly NewProjectDraft _draft = new();
    private DBUnitSet? _importedUnits;

    public NewProjectViewModel(
        IDialogService dialogService,
        IMaterialService materialService,
        INewProjectAppService newProjectAppService)
    {
        _dialogService = dialogService;
        _materialService = materialService;
        _newProjectAppService = newProjectAppService;

        _draft.PropertyChanged += OnDraftPropertyChanged;

        LoadMaterial();
    }

    public event Action<bool?>? RequestClose;

    public NewProjectDraft Draft => _draft;

    public ObservableCollection<ConcreteMaterial> ConcreteMaterials { get; } = new();
    public ObservableCollection<SteelMaterial> SteelMaterials { get; } = new();

    private bool CanCreateProject()
    {
        return Draft.HasRequiredValues && _importedUnits is not null;
    }

    [RelayCommand]
    private void BrowseFolder()
    {
        var selectedPath = _dialogService.PickFolder();
        if (!string.IsNullOrWhiteSpace(selectedPath))
        {
            Draft.FolderPath = selectedPath!;
        }
    }

    [RelayCommand]
    private void BrowseFile()
    {
        var selectedPath = _dialogService.PickAccessDatabaseFile();
        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            return;
        }

        try
        {
            var units = _newProjectAppService.ReadUnits(selectedPath!);

            _importedUnits = units;
            Draft.ImportFile = selectedPath!;
            Draft.LengthUnit = UnitUtils.ToDisplayString(units.LengthUnit);
            Draft.ForceUnit = UnitUtils.ToDisplayString(units.ForceUnit);
        }
        catch (Exception exception)
        {
            _importedUnits = null;
            Draft.ImportFile = string.Empty;
            Draft.LengthUnit = string.Empty;
            Draft.ForceUnit = string.Empty;

            _dialogService.ShowError("BimStructure", $"Khong the doc file Access.{Environment.NewLine}{exception.Message}");
        }

        CreateProjectCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanCreateProject))]
    private void CreateProject()
    {
        _newProjectAppService.CreateProject(new CreateProjectRequest
        {
            ProjectName = Draft.ProjectName,
            FolderPath = Draft.FolderPath,
            ImportFile = Draft.ImportFile,
            Concrete = Draft.SelectedConcreteMaterial,
            Steel = Draft.SelectedSteelMaterial
        });

        RequestClose?.Invoke(true);
    }

    [RelayCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(false);
    }

    private void LoadMaterial()
    {
        ConcreteMaterials.Clear();
        foreach (var concreteMaterial in _materialService.GetConcreteMaterials())
        {
            ConcreteMaterials.Add(concreteMaterial);
        }

        SteelMaterials.Clear();
        foreach (var steelMaterial in _materialService.GetSteelMaterials())
        {
            SteelMaterials.Add(steelMaterial);
        }

        Draft.SelectedConcreteMaterial = ConcreteMaterials.FirstOrDefault();
        Draft.SelectedSteelMaterial = SteelMaterials.FirstOrDefault();
        CreateProjectCommand.NotifyCanExecuteChanged();
    }

    private void OnDraftPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(NewProjectDraft.ProjectName)
            or nameof(NewProjectDraft.FolderPath)
            or nameof(NewProjectDraft.ImportFile)
            or nameof(NewProjectDraft.SelectedConcreteMaterial)
            or nameof(NewProjectDraft.SelectedSteelMaterial))
        {
            CreateProjectCommand.NotifyCanExecuteChanged();
        }
    }
}
