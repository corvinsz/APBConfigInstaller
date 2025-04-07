using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Views.Components;

public class If : ContentControl
{
	public static readonly StyledProperty<bool> ConditionProperty =
		AvaloniaProperty.Register<If, bool>(nameof(Condition), false);

	public bool Condition
	{
		get => GetValue(ConditionProperty);
		set => SetValue(ConditionProperty, value);
	}

	public static readonly StyledProperty<Control> TrueProperty =
		AvaloniaProperty.Register<If, Control>(nameof(True));

	public Control True
	{
		get => GetValue(TrueProperty);
		set => SetValue(TrueProperty, value);
	}

	public static readonly StyledProperty<Control> FalseProperty =
		AvaloniaProperty.Register<If, Control>(nameof(False));

	public Control False
	{
		get => GetValue(FalseProperty);
		set => SetValue(FalseProperty, value);
	}

	static If()
	{
		ConditionProperty.Changed.AddClassHandler<If>((x, e) => x.UpdateContent());
		TrueProperty.Changed.AddClassHandler<If>((x, e) => x.UpdateContent());
		FalseProperty.Changed.AddClassHandler<If>((x, e) => x.UpdateContent());
	}

	private void UpdateContent()
	{
		Content = Condition ? True : False;
	}
}
