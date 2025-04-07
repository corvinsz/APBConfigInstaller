using APBConfigInstaller.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace APBConfigInstaller.Views;

public partial class SettingsView : UserControl
{
	public SettingsView() : this(App.Services.GetRequiredService<SettingsViewModel>())
	{
		InitializeComponent();
	}

	// DI constructor
	public SettingsView(SettingsViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}
}