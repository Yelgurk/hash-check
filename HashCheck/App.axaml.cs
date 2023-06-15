using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using HashCheck.Models;
using HashCheck.ViewModels;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

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
                services.AddSingleton<AnalysisResult>();
                services.AddSingleton<FilesComparingResult>();
                services.AddTransient<IWindowContentService, WindowContentService>();
                services.AddSingleton<HashComputator>();
                services.AddSingleton<SettingFile>();
                services.AddSingleton<Settings>();
            })
            .Build();

        Host.Services.GetRequiredService<MainWindow>().Content = Host.Services.GetRequiredService<FileAwait>();
        Host.Services.GetRequiredService<Settings>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Host!.Services.GetRequiredService<MainWindow>();

            if (desktop.Args.Contains(Program.ServerArg))
                desktop!.Exit += (sender, args) => Program.IPAPI_server.ShutdownAsync();

            if (desktop.Args[0] != Program.ServerArg)
                App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(desktop.Args.Take(desktop.Args.Length - 1).ToArray());
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
        App.Host!.Services.GetRequiredService<MainWindow>()!.SetContent(App.Host!.Services.GetRequiredService<T>()!);
        return App.Host!.Services.GetRequiredService<T>()!;
    }
}