﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using APBConfigInstaller.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace APBConfigInstaller.Views;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : System.Windows.Controls.UserControl
{
    // Parameterless constructor for XAML
    public SettingsView() : this(App.Services.GetRequiredService<SettingsViewModel>())
    {
    }

    // DI constructor
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
