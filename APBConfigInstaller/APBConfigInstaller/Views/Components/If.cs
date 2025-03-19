using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace APBConfigInstaller.Views.Components;

public class If : ContentControl
{
    public static readonly DependencyProperty ConditionProperty =
        DependencyProperty.Register(nameof(Condition), typeof(bool), typeof(If),
            new PropertyMetadata(false, OnContentDependentPropertyChanged));

    public bool Condition
    {
        get => (bool)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly DependencyProperty TrueProperty =
        DependencyProperty.Register(nameof(True), typeof(UIElement), typeof(If),
            new PropertyMetadata(null, OnContentDependentPropertyChanged));

    public UIElement True
    {
        get => (UIElement)GetValue(TrueProperty);
        set => SetValue(TrueProperty, value);
    }

    public static readonly DependencyProperty FalseProperty =
        DependencyProperty.Register(nameof(False), typeof(UIElement), typeof(If),
            new PropertyMetadata(null, OnContentDependentPropertyChanged));

    public UIElement False
    {
        get => (UIElement)GetValue(FalseProperty);
        set => SetValue(FalseProperty, value);
    }

    private static void OnContentDependentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is If currentIf)
        {
            currentIf.UpdateContent();
        }
    }

    private void UpdateContent()
    {
        Content = Condition ? True : False;
    }
}