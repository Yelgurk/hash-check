using Avalonia.Controls;
using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() : this(new WindowContentService())
        { }

        public MainWindow(IWindowContentService _windowContentService)
        {
            InitializeComponent();
            DataContext = new MainWindowVM() { View = this, WindowContentService = (WindowContentService)_windowContentService };
        }

        public void SetContent(object Content) => this.Content = Content;
    }
}