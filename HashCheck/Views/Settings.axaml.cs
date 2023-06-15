using Avalonia.Controls;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class Settings : UserControl
{
    public Settings() : this(new WindowContentService())
    { }

    public Settings(IWindowContentService _windowContentService)
    {
        InitializeComponent(true);
        this.DataContext = new SettingsVM() { View = this, WindowContentService = (WindowContentService)_windowContentService };
    }
}
