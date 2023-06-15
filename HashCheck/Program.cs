using Avalonia;
using HashCheck.InterProcAPI;
using System;
using System.Linq;
using System.Threading;

namespace HashCheck;

internal class Program
{
    public static readonly string ServerArg = "[server]";
    private static Mutex _mutex = null;
    public static InterProcServer IPAPI_server;

    [STAThread]
    public static void Main(string[] args)
    {
        bool CreatedAppInstatnce;
        const string AppName = "HashCheckApp";
        _mutex = new Mutex(true, AppName, out CreatedAppInstatnce);

        if (!CreatedAppInstatnce)
        {
            if (args.Length > 0)
                new InterProcClient(args);
            Environment.Exit(0);
        }
        else
        {
            IPAPI_server = new InterProcServer();
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(
                    args.Length == 0 ?
                    new string[] { ServerArg } :
                    args.Append(ServerArg).ToArray()
                    );
        }
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}