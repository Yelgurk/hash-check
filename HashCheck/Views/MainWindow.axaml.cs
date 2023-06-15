using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Threading;

namespace HashCheck.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() : this(new WindowContentService())
        { }

        public MainWindow(IWindowContentService _windowContentService)
        {
            InitializeComponent(true);
            this.DataContext = new MainWindowVM() { View = this, WindowContentService = _windowContentService };
        }

        public void SetContent (object Content) => this.Content = Content;
    }
}