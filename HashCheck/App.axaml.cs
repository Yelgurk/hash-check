using System.Collections.Generic;
using System.IO;
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
using Gemstone.Collections.CollectionExtensions;
using HashCheck.Domain;

namespace HashCheck;

public partial class App : Application
{
    private static App? THIS { get; set; }

    public static IHost? Host { get; private set; }

    public static void SetTheme(ThemeVariant theme) => THIS!.RequestedThemeVariant = theme;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        THIS ??= this;

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<FileAwait>();
                services.AddSingleton<AnalysisResult>();
                services.AddSingleton<FilesComparingResult>();
                services.AddTransient<WindowContentService>();
                services.AddSingleton<Settings>();
                services.AddSingleton<HashComputeService>();
            })
            .Build();

        Host.Services.GetRequiredService<MainWindow>().SetContent(Host.Services.GetRequiredService<FileAwait>());
        Host.Services.GetRequiredService<Settings>();
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Host!.Services.GetRequiredService<MainWindow>();
            desktop.Exit += (_, _) => Program.IpApiServer?.ShutdownAsync();

            if (!(desktop.Args?.Length > 0))
            {
                return;
            }

            var files = desktop.Args;
            var forAnalyze = files
                .SelectMany(p =>
                {
                    if (File.Exists(p))
                        return new[] { p };
                    if (Directory.Exists(p))
                        return Directory.EnumerateFiles(p, "*", SearchOption.AllDirectories);
                    return Enumerable.Empty<string>();
                }).ToList();

            var resultHashModels = await Host.Services
                .GetRequiredService<HashComputeService>()
                .ComputeHashesFor(new FilePathsOrDirectoryPath(forAnalyze));

            Host.Services
                .GetRequiredService<WindowContentService>()
                .Set<AnalysisResult>()
                .DataContext?.Cast<AnalysisResultVM>()
                ?.Results.AddRange(resultHashModels);
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