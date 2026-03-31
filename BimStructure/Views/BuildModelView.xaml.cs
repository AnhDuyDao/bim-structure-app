using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class BuildModelView
{
    public BuildModelView(BuildModelViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
