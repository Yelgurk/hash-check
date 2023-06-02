using Avalonia.Controls;
using Avalonia.Input;
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

        AddHandler(DragDrop.DropEvent, (DataContext as FileAwaitVM)!.DropFileAndDir);
        AddHandler(DragDrop.DragOverEvent, (sender, e) => {
            e.DragEffects = e.DragEffects & DragDropEffects.Link;
            if (!e.Data.Contains(DataFormats.FileNames))
                e.DragEffects = DragDropEffects.None;
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}