using Avalonia.Controls;
using Avalonia.Input;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class FilesComparingResult : UserControl
{
    public FilesComparingResult() : this(new WindowContentService())
    { }

    public FilesComparingResult(IWindowContentService windowContentService)
    {
        InitializeComponent(true);
        this.DataContext = new FilesComparingResultVM() { View = this, WindowContentService = (WindowContentService)windowContentService };

        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { DragDropPlace_ChoiceContainer.IsVisible = true; });
    }
}