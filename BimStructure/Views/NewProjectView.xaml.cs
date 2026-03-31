using System;
using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class NewProjectView
{
    public NewProjectView(NewProjectViewModel viewModel)
    {
        DataContext = viewModel;
        viewModel.RequestClose += OnRequestClose;
        InitializeComponent();
        Closed += OnClosed;
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
