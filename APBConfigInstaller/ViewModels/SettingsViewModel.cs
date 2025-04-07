using APBConfigInstaller.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velopack.Sources;
using Velopack;
using Avalonia.Styling;
using SukiUI;

namespace APBConfigInstaller.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
	private readonly IThemeService _themeService;
	public SettingsViewModel(IThemeService themeService)
	{
		_themeService = themeService;

		const string repoUrl = "https://github.com/corvinsz/APBConfigInstaller";
		_um = new UpdateManager(new GithubSource(repoUrl, "", true));

		SelectedTheme = ThemeOptions.First();

	}

	[ObservableProperty]
	private string? _APBDirectory;
	partial void OnAPBDirectoryChanged(string? value)
	{
		APBDirectoryHelperText = !Directory.Exists(APBDirectory) ? "Invalid directory" : string.Empty;
	}

	[ObservableProperty]
	private string _APBDirectoryHelperText;

	public ThemeVariant[] ThemeOptions { get; } =
	[
		ThemeVariant.Default,
		ThemeVariant.Light,
		ThemeVariant.Dark
	];

	[ObservableProperty]
	private ThemeVariant? _selectedTheme;

	partial void OnSelectedThemeChanged(ThemeVariant? value)
	{
		_themeService.SetTheme();
		//_themeService.SetTheme(value);
	}

	[RelayCommand]
	private void Browse()
	{
		//var fbd = new FolderBrowserDialog();
		//fbd.SelectedPath = string.IsNullOrWhiteSpace(APBDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : APBDirectory;

		//if (fbd.ShowDialog() == DialogResult.OK)
		//{
		//	APBDirectory = fbd.SelectedPath;
		//}
	}

	#region velopack_stuff
	private readonly UpdateManager _um;
	private UpdateInfo? _update;

	public bool IsUpdateAvailable => _update is not null;

	[ObservableProperty]
	private int _downloadProgress = 0;

	[RelayCommand]
	private async Task CheckForUpdatesUpdate()
	{
		try
		{
			// ConfigureAwait(true) so that UpdateStatus() is called on the UI thread
			_update = await _um.CheckForUpdatesAsync().ConfigureAwait(true);
			OnPropertyChanged(nameof(IsUpdateAvailable));
		}
		catch (Exception ex)
		{
			// TODO: Log error
			//App.Log.LogError(ex, "Error checking for updates");
			//EventLog.WriteEntry("Application", ex.Message, EventLogEntryType.Error);
		}
	}

	[RelayCommand]
	private async Task ApplyUpdateAndRestart()
	{
		if (_update is null)
		{
			return;
		}

		try
		{
			// ConfigureAwait(true) so that UpdateStatus() is called on the UI thread
			await _um.DownloadUpdatesAsync(_update, (progress) => DownloadProgress = progress).ConfigureAwait(true);
			_um.ApplyUpdatesAndRestart(_update);
		}
		catch (Exception ex)
		{
			// TODO: Log error
			// App.Log.LogError(ex, "Error downloading updates");
		}
	}
	#endregion
}
