using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Themes.Fluent;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class FileAwait : UserControl
{
    public FileAwait() : this(new WindowContentService())
    { }

    public FileAwait(IWindowContentService _windowContentService)
    {
        InitializeComponent(true);
        this.DataContext = new FileAwaitVM() { View = this, WindowContentService = (WindowContentService)_windowContentService };

        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
    }
}
