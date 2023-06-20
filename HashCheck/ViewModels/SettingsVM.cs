using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using HashCheck.Models;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.ViewModels;

public class SettingsVM : VMBase
{
    private StyleModel? _styleSelected;

    public StyleModel StyleSelected
    {
        get => _styleSelected ?? StylesList[0];
        set
        {
            if (SetProperty(ref _styleSelected, value))
            {
                SettingFile.Theme = value;
                SettingFile.SaveSettings(SettingPath);
            }

            SetStyles(value);
            OnPropertyChanged(nameof(value.TransparentSetter));
        }
    } 

    public ObservableCollection<StyleModel> StylesList { get; } = new ObservableCollection<StyleModel>()
    {
        new StyleModel()
        {
            Name = "Light",
            Resource = ThemeVariant.Light,
            WindowBackground = StyleModel.AcrylicBorderGenerator(Brushes.WhiteSmoke.Color),
            ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemePrimary"]! },
            ColorBase = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemeBase"]! },
            IsTransparent = true
        },
        new StyleModel()
        {
            Name = "Dark",
            Resource = ThemeVariant.Dark,
            WindowBackground = StyleModel.AcrylicBorderGenerator(Brushes.Black.Color),
            ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemePrimary"]! },
            ColorBase =  new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemeBase"]! },
            IsTransparent = true
        }
    };

    public SettingFile SettingFile { get; set; }

    private static readonly string SettingFileName = "settings.json";

    public static string SettingPath => $"{AppDomain.CurrentDomain.BaseDirectory}\\{SettingFileName}";

    public SettingsVM()
    {
        SettingFile = App.Host!.Services.GetRequiredService<SettingFile>()!;

        if (!File.Exists(SettingPath))
        {
            SettingFile.Theme = StylesList[0];
            SettingFile.SaveSettings(SettingPath);
            (new WindowContentService() as IWindowContentService).Set<Settings>();
        }
        else
            SettingFile.LoadSettings(SettingPath);

        StyleModel Loaded = StylesList.Where((x) => x.IsEqual(SettingFile.Theme as StyleModel)).SingleOrDefault(StylesList[0]);
        Loaded.IsTransparent = SettingFile.Theme.IsTransparent;
        StyleSelected = Loaded;
    }

    private void SetStyles(StyleModel Style)
    {
        App.Host!.Services.GetRequiredService<MainWindow>()!.SetBackground(Style.WindowBackground);
        App.SetTheme(Style.Resource);
    }
}