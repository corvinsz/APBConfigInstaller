﻿using APBConfigInstaller.ViewModels;
using APBConfigInstaller.Views;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;
using SukiUI.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller;

public static class ServiceCollectionExtensions
{
	public static void AddCommonServices(this IServiceCollection collection)
	{
		// Services
		collection.AddTransient<Services.IThemeService, Services.ThemeService>();
		collection.AddTransient<Services.IModProvider, Services.FileModProvider>();

		// ViewModels
		collection.AddTransient<HomeViewModel>();
		collection.AddTransient<SettingsViewModel>();
		collection.AddTransient<MainWindowViewModel>();
	}
}
