using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BimStructure.Models;
using BimStructure.Services;

namespace BimStructure.ViewModels;

public sealed partial class DuAnMoiViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;

    public DuAnMoiViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        ConcreteMaterials = new ObservableCollection<MaterialOption>
        {
            new("B20"),
            new("B25"),
            new("B30")
        };

        SteelMaterials = new ObservableCollection<MaterialOption>
        {
            new("CB240-T"),
            new("CB300-V"),
            new("CB400-V")
        };

        SelectedConcreteMaterial = ConcreteMaterials[0];
        SelectedSteelMaterial = SteelMaterials[0];
    }

    public event Action<bool?>? RequestClose;

    [ObservableProperty]
    private string _projectName = string.Empty;

    [ObservableProperty]
    private string _folderPath = string.Empty;
    
    [ObservableProperty]
    private string _importFile = string.Empty;

    [ObservableProperty]
    private string _lengthUnit = "m";

    [ObservableProperty]
    private string _forceUnit = "kN";

    [ObservableProperty]
    private MaterialOption? _selectedConcreteMaterial;

    [ObservableProperty]
    private MaterialOption? _selectedSteelMaterial;

    public string ImportFileName => Path.GetFileName(ImportFile);
    public ObservableCollection<MaterialOption> ConcreteMaterials { get; }
    public ObservableCollection<MaterialOption> SteelMaterials { get; }
    public bool CanCreateProject =>
        !string.IsNullOrWhiteSpace(ProjectName) &&
        !string.IsNullOrWhiteSpace(FolderPath) &&
        !string.IsNullOrWhiteSpace(ImportFile);

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
        if (!string.IsNullOrWhiteSpace(selectedPath))
        {
            ImportFile = selectedPath!;
        }
    }

    [RelayCommand(CanExecute = nameof(CanCreateProject))]
    private void CreateProject()
    {
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
}
