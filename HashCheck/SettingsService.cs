using Avalonia.Controls.Shapes;
using HashCheck.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HashCheck;

public static class SettingsService
{
    public static readonly string DefaultSettingFileName = "settings.json";
    public static readonly string DefaultSettingFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{DefaultSettingFileName}";

    public static SettingsModel? SettingsFile { get; private set; }

    public static void SaveSettingsIntoJSON(List<SelectableHashModel> AllHashModels, SelectableThemeModel SelectedTheme, string SettingFilePath)
    {
        using FileStream fs = new FileStream(SettingFilePath, FileMode.Create);
        JsonSerializer.Serialize(fs, SettingsFile = new SettingsModel() { SelectableHashModels = AllHashModels, ThemeModel = (SelectableThemeModel)SelectedTheme });
    }

    public static void SaveSettingIntoJSON(List<SelectableHashModel> AllHashModels, SelectableThemeModel SelectedTheme) =>
        SaveSettingsIntoJSON(AllHashModels, SelectedTheme, DefaultSettingFilePath);

    public static bool LoadSettingsFromJSON(string SettingFilePath)
    {
        if (File.Exists(SettingFilePath))
        {
            SettingsFile =  SettingsFile ?? new SettingsModel();

            try
            {
                using (FileStream fs = new FileStream(SettingFilePath, FileMode.Open))
                {
                    JsonNode jObj = JsonObject.Parse(fs)!;
                    SettingsFile.ThemeModel = JsonSerializer.Deserialize<SelectableThemeModel>(jObj!["ThemeModel"])!;
                    SettingsFile.SelectableHashModels = JsonSerializer.Deserialize<List<SelectableHashModel>>(jObj!["SelectableHashModels"])!;
                }
            }
            catch
            {
                SettingsFile = null;
                return false;
            }

            return true;
        }
        else
            return false;
    }

    public static bool LoadSettingsFromJSON() => LoadSettingsFromJSON(DefaultSettingFilePath);
}
