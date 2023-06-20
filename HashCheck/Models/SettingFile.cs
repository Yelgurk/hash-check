using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Models.Interface;

namespace HashCheck.Models;

public partial class SettingFile : ObservableObject, IDisposable
{
    public ObservableCollection<HashModel> Hashes { get; set; }

    public IStyleModel Theme { get; set; }

    [JsonConstructor]
    public SettingFile() { }

    public void SaveSettings(string path)
    {
        try
        {
            using FileStream fs = new FileStream(path, FileMode.Create);
            JsonSerializer.Serialize(fs, this);
        }
        catch { }
    }

    public void LoadSettings(string path)
    {
        bool isDataValid = true;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            JsonNode jObj = JsonObject.Parse(fs)!;
            Theme = JsonSerializer.Deserialize<StyleModel>(jObj!["Theme"]);

            if (JsonSerializer.Deserialize<ObservableCollection<HashModel>>(jObj!["Hashes"])!.Count == this.Hashes.Count)
            {
                foreach (HashModel progHash in this.Hashes)
                    foreach (HashModel settHash in JsonSerializer.Deserialize<ObservableCollection<HashModel>>(jObj!["Hashes"])!)
                        if (progHash.HashName == settHash.HashName)
                            progHash.LoadSelectState = settHash.IsSelected;
            }
            else
                isDataValid = false;
        }

        if (!isDataValid)
            this.SaveSettings(path);
    }

    public void Dispose() => Hashes.Clear();
}
