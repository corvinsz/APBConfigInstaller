using APBConfigInstaller.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Models;

[JsonConverter(typeof(ModBaseConverter))]
public abstract class ModBase : ObservableObject
{
	public string? DisplayName { get; set; }
	public Category Category { get; set; }

	public bool IsApplying { get; set; } = false;

	public async Task ApplyAsync()
	{
		try
		{
			IsApplying = true;
			await Task.Run(() => InternalApply());
		}
		catch
		{
			throw;
		}
		finally
		{
			IsApplying = false;
		}
	}
	protected abstract void InternalApply();
}
