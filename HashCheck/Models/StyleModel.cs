using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Models.Interface;
using HashCheck.ViewModels;
using System.Text.Json.Serialization;

namespace HashCheck.Models
{
    public partial class StyleModel : ObservableObject, IStyleModel
    {
        private static int _counter = 0;
        public StyleModel() => Index = _counter++;

        [JsonConstructor]
        public StyleModel(int Index, string Name, bool IsTransparent)
        {
            this.Index = Index;
            this.Name = Name;
            this.IsTransparent = IsTransparent;
        }

        public int Index { get; set; } 

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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TransparentSetter))]
        private bool isTransparent;

        public bool TransparentSetter
        {
            get
            {
                WindowBackground.Material.MaterialOpacity = IsTransparent ? 0 : 0.7;
                return IsTransparent;
            }
            set
            {
                if (SetProperty(ref isTransparent, value))
                    SettingsVM.SettingFile.SaveSettings(SettingsVM.SettingPath);
            }
        }

        public bool IsEqual(StyleModel? right) => right != null && this.Index == right.Index && this.Name == right.Name;

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
}
