using APBConfigInstaller.Models;
using APBConfigInstaller.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
	//private readonly ISnackbarMessageQueue _snackbarMessageQueue;
	private readonly IModProvider _modProvider;
	private IList<ModBase> _mods;
	public HomeViewModel(/*ISnackbarMessageQueue snackbarMessageQueue,*/ IModProvider modProvider)
	{
		//_snackbarMessageQueue = snackbarMessageQueue;
		_modProvider = modProvider;
	}

	[RelayCommand]
	private void LoadMods()
	{
		_mods = _modProvider.GetMods();
		GroupedMods = _mods.GroupBy(mod => mod.Category).ToDictionary(group => group.Key, group => group.ToList());
		//_snackbarMessageQueue.Enqueue("message.Message", "Details", OnShowErrorDetails, "message.Details");
		//_snackbarMessageQueue.Enqueue("Mods loaded successfully!");
	}

	private void OnShowErrorDetails(object argument)
	{
		throw new NotImplementedException();
	}

	[ObservableProperty]
	private IDictionary<Category, List<ModBase>> _groupedMods;

	[RelayCommand]
	private async Task ApplyMod(ModBase mod)
	{
		try
		{
			await Task.Run(() =>
			{
				mod.Apply();
				//_snackbarMessageQueue.Enqueue($"Applied mod: {mod.DisplayName}");
			});
		}
		catch (Exception ex)
		{
			//_snackbarMessageQueue.Enqueue($"Failed to apply mod: {mod.DisplayName} - {ex.Message}");
		}
	}
}
