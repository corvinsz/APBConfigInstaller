using Material.Styles.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Services;

public interface ISnackbarMessageService
{
	void ShowMessage(string message);
}

public class SnackbarMessageService : ISnackbarMessageService
{
	public void ShowMessage(string message)
	{
		SnackbarHost.Post(message, null, Avalonia.Threading.DispatcherPriority.Normal);
	}
}
