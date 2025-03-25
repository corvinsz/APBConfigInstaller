using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APBConfigInstaller.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using Velopack;
using Velopack.Sources;

namespace APBConfigInstaller.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;

        const string repoUrl = "https://github.com/corvinsz/APBConfigInstaller/tree/v0.1.0";
        _um = new UpdateManager(new GithubSource(repoUrl, "", false));
    }

    [ObservableProperty]
    private string _APBDirectory;
    partial void OnAPBDirectoryChanged(string value)
    {
        if (!Directory.Exists(APBDirectory))
        {
            APBDirectoryHelperText = "Invalid directory";
        }
        else
        {
            APBDirectoryHelperText = string.Empty;
        }
    }

    [ObservableProperty]
    private string _APBDirectoryHelperText;

    public IEnumerable<BaseTheme> ThemeOptions { get; } = Enum.GetValues(typeof(BaseTheme)).Cast<BaseTheme>().ToList();

    [ObservableProperty]
    private BaseTheme _selectedTheme;

    partial void OnSelectedThemeChanged(BaseTheme value)
    {
        _themeService.SetTheme(value);
    }

    [RelayCommand]
    private void Browse()
    {
        var fbd = new FolderBrowserDialog();
        fbd.SelectedPath = string.IsNullOrWhiteSpace(APBDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : APBDirectory;

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            APBDirectory = fbd.SelectedPath;
        }
    }

    #region velopack_stuff
    private readonly UpdateManager _um;
    private UpdateInfo? _update;

    public bool IsUpdateAvailable => _update is not null;

    [ObservableProperty]
    private int _downloadProgress = 0;

    [RelayCommand]
    private async Task CheckForUpdates()
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
        }
    }

    [RelayCommand(CanExecute = nameof(IsUpdateAvailable))]
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
