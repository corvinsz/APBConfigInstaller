using System.Linq;
using APBConfigInstaller.ViewModels;
using APBConfigInstaller.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace APBConfigInstaller;

public partial class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public static ServiceProvider Services { get; private set; } = null!;

	public override void OnFrameworkInitializationCompleted()
	{
		// If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
		// Without this line you will get duplicate validations from both Avalonia and CT
		BindingPlugins.DataValidators.RemoveAt(0);

		// Register all the services needed for the application to run
		var collection = new ServiceCollection();
		collection.AddCommonServices();

		// Creates a ServiceProvider containing services from the provided IServiceCollection
		Services = collection.BuildServiceProvider();

		var vm = Services.GetRequiredService<MainWindowViewModel>();
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = vm
			};
		}

		base.OnFrameworkInitializationCompleted();
	}

	private void DisableAvaloniaDataAnnotationValidation()
	{
		// Get an array of plugins to remove
		var dataValidationPluginsToRemove =
			BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

		// remove each entry found
		foreach (var plugin in dataValidationPluginsToRemove)
		{
			BindingPlugins.DataValidators.Remove(plugin);
		}
	}
}