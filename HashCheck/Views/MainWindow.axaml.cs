using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using System.Threading;

namespace HashCheck.Views
{
    public partial class MainWindow : Window
    {
        private static Mutex _mutex = null;

        public MainWindow() : this(new WindowContentService())
        { }

        public MainWindow(IWindowContentService _windowContentService)
        {
            this.InitializeComponent();
            this.DataContext = new MainWindowVM() { View = this, WindowContentService = _windowContentService };

            const string appName = "HashCheckApp";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime newInstance)
                if (!createdNew)
                    newInstance.Shutdown();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SetContent (object Content) => this.Content = Content;
    }
}