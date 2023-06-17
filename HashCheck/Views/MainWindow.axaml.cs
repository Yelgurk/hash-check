using Avalonia.Controls;
using Avalonia.Media;
using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class MainWindow : Window
    {
        public static ExperimentalAcrylicBorder DarkWindowBackground { get; } = new ExperimentalAcrylicBorder()
        {
            Material = new ExperimentalAcrylicMaterial()
            {
                BackgroundSource = AcrylicBackgroundSource.Digger,
                TintColor = Brushes.Black.Color,
                TintOpacity = 1,
                MaterialOpacity = 0
            },
            IsHitTestVisible = false
        };

        public static ExperimentalAcrylicBorder LightWindowBackground { get; } = new ExperimentalAcrylicBorder()
        {
            Material = new ExperimentalAcrylicMaterial()
            {
                BackgroundSource = AcrylicBackgroundSource.Digger,
                TintColor = Brushes.White.Color,
                TintOpacity = 1,
                MaterialOpacity = 0
            },
            IsHitTestVisible = false
        };

        public MainWindow() : this(new WindowContentService())
        { }

        public MainWindow(IWindowContentService _windowContentService)
        {
            InitializeComponent();
            DataContext = new MainWindowVM() { View = this, WindowContentService = (WindowContentService)_windowContentService };
            
        }

        public void SetContent(object Content) => this.ContentPresenter.Content = Content;
        public void SetBackground(ExperimentalAcrylicBorder Background)
        {
            this.BackgroundBlur.Children.Clear();
            this.BackgroundBlur.Children.Add(Background);
        }
    }
}