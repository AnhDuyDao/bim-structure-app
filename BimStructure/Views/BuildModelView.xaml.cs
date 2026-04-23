using BimStructure.ViewModels;

namespace BimStructure.Views;

public sealed partial class BuildModelView
{
    public BuildModelView(BuildModelViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Loaded += async (_, _) =>
        {
            await viewModel.LoadAsync();
        };
    }
}
