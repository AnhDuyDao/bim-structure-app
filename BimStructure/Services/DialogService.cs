using Microsoft.Win32;
using WindowsAPICodePack.Dialogs;

namespace BimStructure.Services;

public sealed class DialogService : IDialogService
{
    public string? PickFolder()
    {
        var dialog = new CommonOpenFileDialog
        {
            IsFolderPicker = true
        };

        return dialog.ShowDialog() == CommonFileDialogResult.Ok
            ? dialog.FileName
            : null;
    }

    public string? PickAccessDatabaseFile()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Access Database (*.accdb;*.mdb)|*.accdb;*.mdb|Access 2007+ (*.accdb)|*.accdb|Access 2003 (*.mdb)|*.mdb",
            CheckFileExists = true,
            Multiselect = false
        };

        return dialog.ShowDialog() == true
            ? dialog.FileName
            : null;
    }
}
