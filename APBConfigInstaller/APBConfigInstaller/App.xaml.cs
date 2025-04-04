﻿using APBConfigInstaller.Services;
using APBConfigInstaller.ViewModels;
using APBConfigInstaller.Views;

using CommunityToolkit.Mvvm.Messaging;

using MaterialDesignThemes.Wpf;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.Logging;

using System.Windows;
using System.Windows.Threading;

using Velopack;

namespace APBConfigInstaller;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            VelopackApp.Build()
                       .SetLogger(new Services.WindowsEventLogLogger())
                       .Run();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show("Unhandled exception: " + ex.ToString());
        }

        MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task MainAsync(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        await host.StartAsync().ConfigureAwait(true);

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();

        await host.StopAsync().ConfigureAwait(true);
    }

    public static IServiceProvider Services { get; private set; }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
            => configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {
            // Add Services
            services.AddSingleton<IModProvider, FileModProvider>();
            services.AddSingleton<IThemeService, ThemeService>();

            // Add Views & ViewModels
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<SettingsView>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<HomeView>();
            services.AddSingleton<HomeViewModel>();


            services.AddSingleton<WeakReferenceMessenger>();
            services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

            services.AddSingleton(_ => Current.Dispatcher);

            services.AddTransient<ISnackbarMessageQueue>(provider =>
            {
                Dispatcher dispatcher = provider.GetRequiredService<Dispatcher>();
                return new SnackbarMessageQueue(TimeSpan.FromSeconds(3.0), dispatcher);
            });

            Services = services.BuildServiceProvider();
        });
}
