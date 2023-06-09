using HashCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.ViewModels
{
    public partial class SettingsVM : VMBase
    {
        public SettingFile SettingFile { get; init; }

        private static readonly string SettingFileName = "settings.json";

        public static string SettingPath => $"{AppDomain.CurrentDomain.BaseDirectory}\\{SettingFileName}";

        public SettingsVM()
        {
            SettingFile = App.Host!.Services.GetRequiredService<SettingFile>()!;

            if (!File.Exists(SettingPath))
                SettingFile.SaveSettings(SettingPath);
            else
                SettingFile.LoadSettings(SettingPath);
        }
    }
}
