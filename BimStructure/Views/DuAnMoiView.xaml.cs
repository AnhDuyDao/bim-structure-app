using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class DuAnMoiView
{
    public DuAnMoiView(DuAnMoiViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}