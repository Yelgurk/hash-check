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
using HashCheck.Models;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace HashCheck;

public partial class App : Application
{
    [DllImport("shell32.dll", SetLastError = true)]
    public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

    [DllImport("shell32.dll", SetLastError = true)]
    public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

    public static void OpenFolderAndSelectItem(string folderPath, string file)
    {
        IntPtr nativeFolder;
        uint psfgaoOut;
        SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

        if (nativeFolder == IntPtr.Zero)
            return;

        IntPtr nativeFile;
        SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

        IntPtr[] fileArray;
        if (nativeFile == IntPtr.Zero)
            fileArray = new IntPtr[0];
        else
            fileArray = new IntPtr[] { nativeFile };

        SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

        Marshal.FreeCoTaskMem(nativeFolder);
        if (nativeFile != IntPtr.Zero)
            Marshal.FreeCoTaskMem(nativeFile);
    }

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
        App.Host!.Services.GetRequiredService<MainWindow>()!.SetContent(App.Host!.Services.GetRequiredService<T>()!);
        return App.Host!.Services.GetRequiredService<T>()!;
    }
}