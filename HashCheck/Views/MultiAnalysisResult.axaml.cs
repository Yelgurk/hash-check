using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class MultiAnalysisResult : UserControl
    {
        public MultiAnalysisResult() : this(new WindowContentService())
        { }

        public MultiAnalysisResult(IWindowContentService _windowContentService)
        {
            this.InitializeComponent();
            this.DataContext = new MultiAnalysisResultVM() { View = this, WindowContentService = _windowContentService };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
