using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class FilesComparingResult : UserControl
    {
        public FilesComparingResult() : this(new WindowContentService())
        { }

        public FilesComparingResult(IWindowContentService _windowContentService)
        {
            this.InitializeComponent();
            this.DataContext = new FilesComparingResultVM() { View = this, WindowContentService = _windowContentService };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
