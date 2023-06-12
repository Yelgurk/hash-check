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
        this.InitializeComponent();
        this.DataContext = new FileAwaitVM() { View = this, WindowContentService = _windowContentService };

        this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });

        /*
        var source = PathGeometry.Parse("M3 6C3 4.34315 4.34315 3 6 3H14C15.6569 3 17 4.34315 17 6V14C17 15.6569 15.6569 17 14 17H6C4.34315 17 3 15.6569 3 14V6ZM6 4C4.89543 4 4 4.89543 4 6V14C4 15.1046 4.89543 16 6 16H14C15.1046 16 16 15.1046 16 14V6C16 4.89543 15.1046 4 14 4H6Z");
        var sourceFlattened = source.GetFlattenedPathGeometry();


        var target = PathGeometry.Parse("M10 3C6.13401 3 3 6.13401 3 10C3 13.866 6.13401 17 10 17C13.866 17 17 13.866 17 10C17 6.13401 13.866 3 10 3ZM2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10Z");
        var targetFlattened = target.GetFlattenedPathGeometry();

        var easing = new CircularEaseInOut();
        var cache = Morph.ToCache(sourceFlattened, targetFlattened, 0.01, easing);

        var path = this.FindControl<Path>("path");
        var slider = this.FindControl<Slider>("slider");

        slider.Minimum = 0;
        slider.Maximum = cache.Count - 1;
        slider.SmallChange = 1;
        slider.LargeChange = 10;
        slider.TickFrequency = 1;
        slider.IsSnapToTickEnabled = true;
        slider.PropertyChanged += (_, args) =>
        {
            if (args.Property == Slider.ValueProperty)
            {
                var index = (int)slider.Value;
                path.Data = cache[index];
            }
        };
        slider.Value = 0;
        path.Data = cache[0];

        // DEBUG
        //path.Data = source;
        //path.Data = sourceFlattened;
        //path.Data = target;
        //path.Data = targetFlattened;

        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1 / 60.0);
        timer.Tick += (sender, e) =>
        {
            if (slider.Value < slider.Maximum)
            {
                slider.Value++;
            }
            else
            {
                slider.Value = slider.Minimum;
            }
        };

        timer.Start();
        */
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}