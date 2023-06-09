using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.Models;

public partial class SettingFile : ObservableObject, IDisposable
{
    public enum ProgTheme { Light, Dark, System };

    public ObservableCollection<IHashModel> Hashes { get; private init; }

    public ProgTheme Theme { get; set; }

    [JsonConstructor]
    public SettingFile()
    {
        this.Hashes = new ObservableCollection<IHashModel>();
    }

    public SettingFile(HashComputator hashComputator)
    {
        this.Hashes = hashComputator.Hashes;
        this.Theme = ProgTheme.Light;
    }

    public void SaveSettings(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            JsonSerializer.Serialize<SettingFile>(fs, this);
    }

    public void LoadSettings(string path)
    {
        bool IsDataValid = true;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        using (SettingFile sf = JsonSerializer.Deserialize<SettingFile>(fs)!)
        {
            this.Theme = sf.Theme;
            if (sf.Hashes.Count != this.Hashes.Count)
                IsDataValid = false;
            else
            {
                this.Hashes
                    .Select((progHash) =>
                    {
                        sf.Hashes
                        .Select((settHash) =>
                        {
                            if (progHash.HashName == settHash.HashName)
                                progHash.IsSelected = settHash.IsSelected;
                            return settHash;
                        });
                        return progHash;
                    });
            }
        }

        if (!IsDataValid)
            this.SaveSettings(path);
    }

    public void Dispose() => Hashes.Clear();
}
