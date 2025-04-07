using Avalonia;
using Avalonia.Styling;
using SukiUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Services;

public interface IThemeService
{
	void SetTheme();
}

public class ThemeService : IThemeService
{
	private readonly SukiTheme _theme;

	public ThemeService()
	{
		_theme = SukiTheme.GetInstance();
	}
	public void SetTheme()
	{
		_theme.SwitchBaseTheme();
		//if (Application.Current is not null)
		//{
		//	Application.Current.RequestedThemeVariant = themeVariant;
		//}
	}
}
