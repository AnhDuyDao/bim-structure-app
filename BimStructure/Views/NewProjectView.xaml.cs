using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class NewProjectView
{
    public NewProjectView(NewProjectViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        
        viewModel.RequestClose += OnRequestClose;
        Closed += OnClosed;
        
        Loaded += (_, _) =>
        {
            if (viewModel.LoadAsyncCommand.CanExecute(null))
                viewModel.LoadAsyncCommand.Execute(null);
        };
    }

    private void OnRequestClose(bool? dialogResult)
    {
        DialogResult = dialogResult;
        Close();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        if (DataContext is NewProjectViewModel viewModel)
        {
            viewModel.RequestClose -= OnRequestClose;
        }

        Closed -= OnClosed;
    }
}
