using Avalonia.Controls;
using Avalonia.Input;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class AnalysisResult : UserControl
{
    public AnalysisResult() : this(new WindowContentService())
    { }

    public AnalysisResult(IWindowContentService _windowContentService)
    {
        this.InitializeComponent(true);
        this.DataContext = new AnalysisResultVM() { View = this, WindowContentService = (WindowContentService)_windowContentService };


        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
    }
}