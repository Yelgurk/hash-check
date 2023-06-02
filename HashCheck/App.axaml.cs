using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using HashCheck.ViewModels;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avalonia.Controls;
using Avalonia.Input;

namespace HashCheck;

public partial class App : Application
{
    public static IHost? Host { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<FileAwait>();
                services.AddSingleton<SingleAnalysisResult>();
                services.AddSingleton<MultiAnalysisResult>();
                services.AddSingleton<FileAwait>();
                services.AddTransient<IWindowContentService, WindowContentService>();
                services.AddSingleton<HashComputator>();
            })
            .Build();

        Host.Services.GetRequiredService<MainWindow>().Content = Host.Services.GetRequiredService<FileAwait>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
            desktop.MainWindow = Host!.Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}

public interface IWindowContentService
{
    public T Set<T>() where T : notnull;
}

public class WindowContentService : IWindowContentService
{
    T IWindowContentService.Set<T>()
    {
        App.Host!.Services.GetRequiredService<MainWindow>()!.Content = App.Host!.Services.GetRequiredService<T>()!;
        return App.Host!.Services.GetRequiredService<T>()!;
    }
}