using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

namespace APBConfigInstaller.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _APBDirectory;
    partial void OnAPBDirectoryChanged(string value)
    {
        if (!File.Exists(APBDirectory))
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

    [ObservableProperty]
    private BaseTheme _selectedTheme;

    [RelayCommand]
    private void Browse()
    {
        var fbd = new FolderBrowserDialog();
        if (string.IsNullOrWhiteSpace(APBDirectory))
        {
            fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }
        else
        {
            fbd.SelectedPath = APBDirectory;
        }

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            APBDirectory = fbd.SelectedPath;
        }
    }
}
