using System.Windows.Controls;

using APBConfigInstaller.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace APBConfigInstaller.Views;

/// <summary>
/// Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    // Parameterless constructor for XAML
    public HomeView() : this(App.Services.GetRequiredService<HomeViewModel>())
    {
    }

    // DI constructor
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
