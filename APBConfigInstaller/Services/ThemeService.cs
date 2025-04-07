using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Services;

public interface IThemeService
{
	void SetTheme(BaseThemeMode themeMode);
}

public class ThemeService : IThemeService
{
	private static readonly MaterialThemeBase _materialThemeStyles =
		Application.Current!.LocateMaterialTheme<MaterialThemeBase>();
	public void SetTheme(BaseThemeMode themeMode)
	{
		_materialThemeStyles.BaseTheme = themeMode;
	}
}
