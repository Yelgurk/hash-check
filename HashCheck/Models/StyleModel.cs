using Avalonia.Markup.Xaml.Styling;
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
    }
}
