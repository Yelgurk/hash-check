using Avalonia.Controls;
using Avalonia.Input;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class AnalysisResult : UserControl
{
    public AnalysisResult() : this(new WindowContentService())
    { }

    public AnalysisResult(WindowContentService windowContentService)
    {
        this.InitializeComponent(true);
        this.DataContext = new AnalysisResultVM() { View = this, WindowContentService = windowContentService };
        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
    }
}