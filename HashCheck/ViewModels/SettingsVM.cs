using Avalonia.Markup.Xaml.Styling;
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
            get => _styleSelected ?? StylesList[0];
            set
            {
                if (SetProperty(ref _styleSelected, value))
                {
                    SettingFile.Theme = StylesList.IndexOf(value);
                    SettingFile.SaveSettings(SettingPath);
                }

                SetStyles(value.Resource);
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
                new StyleModel() { Name = "Светлая", Resource = CreateStyle("avares://Citrus.Avalonia/Citrus.xaml") },
                new StyleModel() { Name = "Тёмная", Resource = CreateStyle("avares://Citrus.Avalonia/Rust.xaml") },
                new StyleModel() { Name = "Система (пока нет)", Resource = CreateStyle("avares://Citrus.Avalonia/Magma.xaml") }
            };

            this.StyleSelected = StylesList[SettingFile.Theme < StylesList.Count ? SettingFile.Theme : 0];
        }

        private static StyleInclude CreateStyle(string url)
        {
            Uri self = new Uri("resm:Styles?assembly=HashCheck");
            return new StyleInclude(self) { Source = new Uri(url) };
        }

        private void SetStyles(StyleInclude style) => Avalonia.Application.Current!.Styles[0] = style;
    }
}
