using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
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

namespace HashCheck.ViewModels
{
    public partial class SettingsVM : VMBase
    {
        private StyleModel? _styleSelected;

        public StyleModel StyleSelected
        {
            get => _styleSelected ?? new StyleModel() { Name = "debug", Resource = ThemeVariant.Light };
            set
            {
                if (SetProperty(ref _styleSelected, value))
                {
                    SettingFile.Theme = StylesList.IndexOf(value);
                    SettingFile.SaveSettings(SettingPath);
                }

                SetStyles(value);
            }
        }

        public ObservableCollection<StyleModel> StylesList { get; private init; }

        public SettingFile SettingFile { get; init; }

        private static readonly string SettingFileName = "settings.json";

        public static string SettingPath => $"{AppDomain.CurrentDomain.BaseDirectory}\\{SettingFileName}";

        public SettingsVM()
        {
            SettingFile = App.Host!.Services.GetRequiredService<SettingFile>()!;

            if (!File.Exists(SettingPath))
            {
                SettingFile.SaveSettings(SettingPath);
                (new WindowContentService() as IWindowContentService).Set<Settings>();
            }
            else
                SettingFile.LoadSettings(SettingPath);

            StylesList = new ObservableCollection<StyleModel>()
            {
                new StyleModel()
                {
                    Name = "Светлая",
                    Resource = ThemeVariant.Light,
                    WindowBackground = MainWindow.LightWindowBackground,
                    ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemePrimary"]! },
                    ColorBase = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemeBase"]! }
                },
                new StyleModel()
                {
                    Name = "Тёмная",
                    Resource = ThemeVariant.Dark,
                    WindowBackground = MainWindow.DarkWindowBackground,
                    ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemePrimary"]! },
                    ColorBase =  new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemeBase"]! }
                }
            };

            this.StyleSelected = StylesList[SettingFile.Theme < StylesList.Count ? SettingFile.Theme : 0];
        }

        private void SetStyles(StyleModel Style)
        {
            App.Host!.Services.GetRequiredService<MainWindow>()!.SetBackground(Style.WindowBackground);
            App.SetTheme(Style.Resource);
        }
    }
}
