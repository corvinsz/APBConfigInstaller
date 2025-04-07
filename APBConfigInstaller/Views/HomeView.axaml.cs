using APBConfigInstaller.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace APBConfigInstaller.Views;

public partial class HomeView : UserControl
{
	public HomeView() : this(App.Services.GetRequiredService<HomeViewModel>())
	{
		InitializeComponent();
	}

	// DI constructor
	public HomeView(HomeViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}
}