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
    private readonly IAccessService _accessService;
    private readonly IVatLieuService _vatLieuService;

    public DuAnMoiViewModel(
        IDialogService dialogService,
        IAccessService accessService,
        IVatLieuService vatLieuService)
    {
        _dialogService = dialogService;
        _accessService = accessService;
        _vatLieuService = vatLieuService;

        LoadVatLieu();
    }

    public event Action<bool?>? RequestClose;

    [ObservableProperty]
    private string _projectName = "Du_an_1";

    [ObservableProperty]
    private string _folderPath = string.Empty;
    
    [ObservableProperty]
    private string _importFile = string.Empty;

    [ObservableProperty]
    private string _lengthUnit = "m";

    [ObservableProperty]
    private string _forceUnit = "kN";

    public ObservableCollection<VatLieuBeTong> ConcreteMaterials { get; } = new();
    public ObservableCollection<VatLieuThep> SteelMaterials { get; } = new();

    [ObservableProperty]
    private VatLieuBeTong? _selectedConcreteMaterial;

    [ObservableProperty]
    private VatLieuThep? _selectedSteelMaterial;

    public string ImportFileName => Path.GetFileName(ImportFile);
    
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
        try
        {
            _accessService.ValidateDatabase(ImportFile);
        }
        catch (Exception exception)
        {
            _dialogService.ShowError("BimStructure", $"Khong the mo file Access.{Environment.NewLine}{exception.Message}");
            return;
        }

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

    private void LoadVatLieu()
    {
        ConcreteMaterials.Clear();
        foreach (var vatLieuBeTong in _vatLieuService.GetBeTong())
        {
            ConcreteMaterials.Add(vatLieuBeTong);
        }

        SteelMaterials.Clear();
        foreach (var vatLieuThep in _vatLieuService.GetThep())
        {
            SteelMaterials.Add(vatLieuThep);
        }

        SelectedConcreteMaterial = ConcreteMaterials.Count > 0 ? ConcreteMaterials[0] : null;
        SelectedSteelMaterial = SteelMaterials.Count > 0 ? SteelMaterials[0] : null;
    }
}
