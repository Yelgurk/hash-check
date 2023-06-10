using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
        this.InitializeComponent();
        this.DataContext = new FileAwaitVM() { View = this, WindowContentService = _windowContentService };

        Border DDBorder = this.FindControl<Border>("DragDropPlace_Main");

        DDBorder.AddHandler(DragDrop.DropEvent, (o, e) =>
        {
            DDBorder.BorderThickness = new Thickness(0);
            (DataContext as FileAwaitVM)!.DropObjectForHash(o, e);
        });
        DDBorder.AddHandler(DragDrop.DragOverEvent, (o, e) =>
        {
            DDBorder.BorderThickness = new Thickness(5);
            (DataContext as FileAwaitVM)!.DragOverAccess(o, e);
        });
        DDBorder.AddHandler(DragDrop.DragLeaveEvent, (o, e) => DDBorder.BorderThickness = new Thickness(0));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}