using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BimStructure.Services;

namespace BimStructure.ViewModels;

public sealed partial class DuAnMoiViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;

    public DuAnMoiViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [ObservableProperty]
    private string _projectName = string.Empty;

    [ObservableProperty]
    private string _folderPath = string.Empty;
    
    [ObservableProperty]
    private string _importFile = string.Empty;

    public string ImportFileName => Path.GetFileName(ImportFile);

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

    partial void OnImportFileChanged(string value)
    {
        OnPropertyChanged(nameof(ImportFileName));
    }
}
