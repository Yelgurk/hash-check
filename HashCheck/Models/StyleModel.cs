using Avalonia.Markup.Xaml.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
