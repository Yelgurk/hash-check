using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class FileAwait : UserControl
{
    public FileAwait() : this(new WindowContentService())
    { }

    public FileAwait(IWindowContentService _windowContentService)
    {
        this.InitializeComponent();
        this.DataContext = new FileAwaitVM() { View = this, WindowContentService = _windowContentService };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}