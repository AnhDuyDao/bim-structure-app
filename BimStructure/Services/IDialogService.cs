namespace BimStructure.Services;

public interface IDialogService
{
    string? PickFolder();
    string? PickAccessDatabaseFile();
    void ShowError(string title, string message);
}
