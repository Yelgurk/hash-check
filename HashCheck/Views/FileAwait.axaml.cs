using Avalonia;
using Avalonia.Animation.Animators;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventBinder;
using HashCheck.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

[assembly: EventBinder.AssemblyReference]
namespace HashCheck.Views;

public partial class FileAwait : UserControl
{
    public FileAwait() : this(new WindowContentService())
    { }

    public FileAwait(IWindowContentService _windowContentService)
    {
        InitializeComponent(true);
        this.DataContext = new FileAwaitVM() { View = this, WindowContentService = _windowContentService };

        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
    }
}