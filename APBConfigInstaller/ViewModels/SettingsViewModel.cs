﻿using APBConfigInstaller.Services;
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
using Avalonia.Themes.Fluent;
using Avalonia.Styling;
using Material.Styles.Themes.Base;
using Avalonia.Controls;

namespace APBConfigInstaller.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
	private readonly ISnackbarMessageService _snackbarMessageService;
	private readonly IThemeService _themeService;

	public SettingsViewModel(ISnackbarMessageService snackbarMessageService, IThemeService themeService)
	{
		_snackbarMessageService = snackbarMessageService;
		_themeService = themeService;

		const string repoUrl = "https://github.com/corvinsz/APBConfigInstaller";
		_um = new UpdateManager(new GithubSource(repoUrl, "", true));
		CurrentAppVersion = _um.CurrentVersion?.ToString() ?? "n.a.";
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

	public BaseThemeMode[] ThemeOptions { get; } = Enum.GetValues<BaseThemeMode>();

	[ObservableProperty]
	private BaseThemeMode _selectedTheme;

	partial void OnSelectedThemeChanged(BaseThemeMode value)
	{
		_themeService.SetTheme(value);
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

	[ObservableProperty]
	private string _currentAppVersion;

	[RelayCommand]
	private async Task CheckForUpdatesUpdate()
	{
		if (!_um.IsInstalled)
		{
			_snackbarMessageService.ShowMessage("App is not installed");
			return;
		}

		try
		{
			_update = await _um.CheckForUpdatesAsync().ConfigureAwait(true);
			OnPropertyChanged(nameof(IsUpdateAvailable));

			if (IsUpdateAvailable)
			{
				_snackbarMessageService.ShowMessage($"Update available: {_update?.TargetFullRelease.Version.ToString()}");
			}
			else
			{
				_snackbarMessageService.ShowMessage("No updates available");
			}
		}
		catch (Exception ex)
		{
			_snackbarMessageService.ShowMessage(ex.Message);
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
			await _um.DownloadUpdatesAsync(_update, (progress) => DownloadProgress = progress).ConfigureAwait(true);
			_um.ApplyUpdatesAndRestart(_update);
		}
		catch (Exception ex)
		{
			_snackbarMessageService.ShowMessage(ex.Message);
			// TODO: Log error
			// App.Log.LogError(ex, "Error downloading updates");
		}
	}
	#endregion
}
