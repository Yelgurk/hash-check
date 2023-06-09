﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using EventBinder;
using HashCheck.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
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

        this.FindControl<Border>("DragDropPlace_Main").AddHandler(DragDrop.DropEvent, (DataContext as FileAwaitVM)!.DropObjectForHash);
        this.FindControl<Border>("DragDropPlace_Main").AddHandler(DragDrop.DragOverEvent, (DataContext as FileAwaitVM)!.DragOverAccess);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}