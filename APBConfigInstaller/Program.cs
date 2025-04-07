using System;
using Avalonia;
using Velopack;

namespace APBConfigInstaller;

internal sealed class Program
{
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
	{
		try
		{
			// It's important to Run() the VelopackApp as early as possible in app startup.
			VelopackApp.Build()
				.OnFirstRun((v) => { /* Your first run code here */ })
				// TODO impl logger
				//.SetLogger(Log)
				.Run();

			BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
		}
		catch (Exception ex)
		{

			throw;
		}

	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.WithInterFont()
			.LogToTrace();
}
