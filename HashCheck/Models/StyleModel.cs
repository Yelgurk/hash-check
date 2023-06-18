using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HashCheck.Models
{
    public partial class StyleModel : ObservableObject
    {
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
    }
}
