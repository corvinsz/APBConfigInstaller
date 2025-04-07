using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Services;

public interface IThemeService
{
	void SetTheme(ThemeVariant? themeVariant);
}

public class ThemeService : IThemeService
{
	public void SetTheme(ThemeVariant? themeVariant)
	{
		if (Application.Current is not null)
		{
			Application.Current.RequestedThemeVariant = themeVariant;
		}
	}
}
