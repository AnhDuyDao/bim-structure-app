using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BimStructure.Models;
using BimStructure.Services;
using UnitUtils = BimStructure.Utils.UnitUtils;

namespace BimStructure.ViewModels;

public sealed partial class NewProjectViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUnitService _unitService;
    private readonly IMaterialService _materialService;
    private readonly IProjectService _projectService;
    private DBUnitSet? _importedUnits;

    public NewProjectViewModel(
        IDialogService dialogService,
        IUnitService unitService,
        IMaterialService materialService, IProjectService projectService)
    {
        _dialogService = dialogService;
        _unitService = unitService;
        _materialService = materialService;
        _projectService = projectService;

        LoadMaterial();
    }

    public event Action<bool?>? RequestClose;

    [ObservableProperty]
    private string _projectName = "Du_an_1";

    [ObservableProperty]
    private string _folderPath = string.Empty;
    
    [ObservableProperty]
    private string _importFile = string.Empty;

    [ObservableProperty]
    private string _lengthUnit = string.Empty;

    [ObservableProperty]
    private string _forceUnit = string.Empty;

    public ObservableCollection<ConcreteMaterial> ConcreteMaterials { get; } = new();
    public ObservableCollection<SteelMaterial> SteelMaterials { get; } = new();

    [ObservableProperty]
    private ConcreteMaterial? _selectedConcreteMaterial;

    [ObservableProperty]
    private SteelMaterial? _selectedSteelMaterial;

    public string ImportFileName => Path.GetFileName(ImportFile);
    
    public bool CanCreateProject =>
        !string.IsNullOrWhiteSpace(ProjectName) &&
        !string.IsNullOrWhiteSpace(FolderPath) &&
        !string.IsNullOrWhiteSpace(ImportFile) &&
        _importedUnits is not null;

    [RelayCommand]
    private void BrowseFolder()
    {
        var selectedPath = _dialogService.PickFolder();
        if (!string.IsNullOrWhiteSpace(selectedPath))
        {
            FolderPath = selectedPath!;
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
            var units = _unitService.GetUnits(selectedPath!);

            _importedUnits = units;
            ImportFile = selectedPath!;
            LengthUnit = UnitUtils.ToDisplayString(units.LengthUnit);
            ForceUnit = UnitUtils.ToDisplayString(units.ForceUnit);
        }
        catch (Exception exception)
        {
            _importedUnits = null;
            ImportFile = string.Empty;
            LengthUnit = string.Empty;
            ForceUnit = string.Empty;

            _dialogService.ShowError("BimStructure", $"Khong the doc file Access.{Environment.NewLine}{exception.Message}");
        }
    }

    [RelayCommand(CanExecute = nameof(CanCreateProject))]
    private void CreateProject()
    {
        var project = new Project
        {
            Name = ProjectName,
            RootPath = FolderPath,
            DBFileName = ImportFile,
            Concrete = SelectedConcreteMaterial,
            Steel = SelectedSteelMaterial
        };

        _projectService.CreateProject(project);
        RequestClose?.Invoke(true);
    }

    [RelayCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(false);
    }

    partial void OnProjectNameChanged(string value)
    {
        CreateProjectCommand.NotifyCanExecuteChanged();
    }
    
    partial void OnFolderPathChanged(string value)
    {
        CreateProjectCommand.NotifyCanExecuteChanged();
    }

    partial void OnImportFileChanged(string value)
    {
        OnPropertyChanged(nameof(ImportFileName));
        CreateProjectCommand.NotifyCanExecuteChanged();
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

        SelectedConcreteMaterial = ConcreteMaterials.Count > 0 ? ConcreteMaterials[0] : null;
        SelectedSteelMaterial = SteelMaterials.Count > 0 ? SteelMaterials[0] : null;
    }
}
