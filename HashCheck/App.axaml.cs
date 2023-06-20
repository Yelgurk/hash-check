using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using HashCheck.Models;
using HashCheck.ViewModels;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace HashCheck;

public partial class App : Application
{
    private static App? THIS { get; set; }

    public static IHost? Host { get; private set; }

    public static void SetTheme(ThemeVariant theme) => THIS!.RequestedThemeVariant = theme;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        THIS = THIS ?? this;

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<FileAwait>();
                services.AddSingleton<AnalysisResult>();
                services.AddSingleton<FilesComparingResult>();
                services.AddTransient<WindowContentService>();
                services.AddSingleton<SettingFile>();
                services.AddSingleton<Settings>();
            })
            .Build();

        Host.Services.GetRequiredService<MainWindow>().SetContent(Host.Services.GetRequiredService<FileAwait>());
        Host.Services.GetRequiredService<Settings>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Host!.Services.GetRequiredService<MainWindow>();

            if (desktop.Args.Contains(Program.ServerArg))
                desktop!.Exit += (sender, args) => Program.IpApiServer.ShutdownAsync();

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
    public T Set<T>() where T : notnull
    {
        App.Host!.Services.GetRequiredService<MainWindow>()!.SetContent(App.Host!.Services.GetRequiredService<T>()!);
        return App.Host!.Services.GetRequiredService<T>()!;
    }
}