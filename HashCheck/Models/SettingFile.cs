using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.Models;

public partial class SettingFile : ObservableObject, IDisposable
{
    public enum ProgTheme { Light, Dark, System };

    public ObservableCollection<HashModel> Hashes { get; set; }

    public ProgTheme Theme { get; set; }

    [JsonConstructor]
    public SettingFile() { }

    public SettingFile(HashComputator hashComputator)
    {
        this.Hashes = hashComputator.Hashes;
        this.Theme = ProgTheme.Light;
    }

    public void SaveSettings(string path)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
                JsonSerializer.Serialize<SettingFile>(fs, this);
        }
        catch { }
    }

    public void LoadSettings(string path)
    {
        bool IsDataValid = true;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            JsonNode jObj = JsonObject.Parse(fs)!;
            Theme = JsonSerializer.Deserialize<ProgTheme>(jObj!["Theme"]);

            foreach(HashModel progHash in this.Hashes)
                foreach(HashModel settHash in JsonSerializer.Deserialize<ObservableCollection<HashModel>>(jObj!["Hashes"])!)
                    if (progHash.HashName == settHash.HashName)
                        progHash.LoadSelectState = settHash.IsSelected;
        }

        if (!IsDataValid)
        this.SaveSettings(path);
    }

    public void Dispose() => Hashes.Clear();
}
