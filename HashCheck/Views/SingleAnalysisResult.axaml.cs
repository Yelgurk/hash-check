using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class SingleAnalysisResult : UserControl
    {
        public SingleAnalysisResult() : this(new WindowContentService())
        { }

        public SingleAnalysisResult(IWindowContentService _windowContentService)
        {
            this.InitializeComponent();
            this.DataContext = new SingleAnalysisResultVM() { View = this, WindowContentService = _windowContentService };

            this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
