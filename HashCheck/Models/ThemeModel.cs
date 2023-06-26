using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.Models;

public partial class ThemeModel : SelectableThemeModel
{
    private static int _counter = 0;

    public ThemeModel() => Index = _counter++;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ThemeVariant resource;

    [ObservableProperty]
    private ExperimentalAcrylicBorder windowBackground;

    [ObservableProperty]
    private SolidColorBrush colorPrimary;

    [ObservableProperty]
    private SolidColorBrush colorBase;

    private bool isTransparent;

    public override bool IsTransparent
    {
        get
        {
            WindowBackground.Material.MaterialOpacity = isTransparent ? 0 : 0.7;
            return isTransparent;
        }
        set
        {
            if (SetProperty(ref isTransparent, value) && WhenSelected is not null)
                WhenSelected();
        }
    }

    public Action? WhenSelected { get; set; }

    public static ExperimentalAcrylicBorder AcrylicBorderGenerator(Color Background)
    {
        return new ExperimentalAcrylicBorder()
        {
            Material = new ExperimentalAcrylicMaterial()
            {
                BackgroundSource = AcrylicBackgroundSource.Digger,
                TintColor = Background,
                TintOpacity = 1,
                MaterialOpacity = 0
            },
            IsHitTestVisible = false
        };
    }
}
