using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using HashCheck.InterProcAPI.Server;
using HashCheck.InterProcAPI.Client;

namespace HashCheck
{
    internal class Program
    {
        private static Mutex _mutex = null;
        private static StreamServiceServer IPAPI_server;
        private static StreamServiceClient IPAPI_client;

        [STAThread]
        public static void Main(string[] args)
        {
            bool CreatedAppInstatnce;
            const string AppName = "HashCheckApp";
            _mutex = new Mutex(true, AppName, out CreatedAppInstatnce);

            if (!CreatedAppInstatnce)
            {
                IPAPI_client = new StreamServiceClient();
                IPAPI_client.WriteCommand.Execute("");
                //IPAPI_client.WriteCommand.Execute(args[0]);

                Environment.FailFast("already runned");
            }
            else
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
                IPAPI_server = new StreamServiceServer();
                IPAPI_server.SubscribeLogger();
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}