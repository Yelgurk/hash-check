using System;
using System.Linq;
using System.Threading;

using Avalonia;

using HashCheck.InterProcAPI;

namespace HashCheck;

internal abstract class Program
{
    public static InterProcServer? IpApiServer;

    [STAThread]
    public static int Main(string[] args)
    {
        const string appName = "HashCheckApp";
        _ = new Mutex(true, appName, out var createdAppInstance);

        if (!createdAppInstance)
        {
            if (args.Length > 0)
            {
                _ = new InterProcClient(args);
            }
            return 0;
        }

        IpApiServer = new InterProcServer();
        _ = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .StartWithClassicDesktopLifetime(args);

        return 0;
    }
}