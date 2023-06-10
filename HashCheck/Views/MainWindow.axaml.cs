using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() : this(new WindowContentService())
        { }

        public MainWindow(IWindowContentService _windowContentService)
        {
            this.InitializeComponent();
            this.DataContext = new MainWindowVM() { View = this, WindowContentService = _windowContentService };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SetContent (object Content) => this.Content = Content;
    }
}