using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APBConfigInstaller.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

namespace APBConfigInstaller.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;
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
}
