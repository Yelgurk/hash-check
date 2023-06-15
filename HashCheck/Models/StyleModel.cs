using Avalonia.Markup.Xaml.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HashCheck.Models
{
    public partial class StyleModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private StyleInclude resource;
    }
}
