using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HashCheck.Models;
using HashCheck.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HashCheck.Views;

public partial class Settings : UserControl
{
    public Settings() : this(new WindowContentService())
    { }

    public Settings(IWindowContentService _windowContentService)
    {
        InitializeComponent(true);
        this.DataContext = new SettingsVM() { View = this, WindowContentService = _windowContentService };
    }
}