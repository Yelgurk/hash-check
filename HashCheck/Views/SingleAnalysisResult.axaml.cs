using Avalonia.Controls;
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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
