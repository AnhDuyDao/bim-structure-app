using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class BimStructureView
{
    public BimStructureView(BimStructureViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}