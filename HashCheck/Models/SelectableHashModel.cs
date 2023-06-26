using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HashCheck.Models;

public partial class SelectableHashModel : ObservableObject
{
    public HashAlgorithmType HashAlgorithm { get; set; }

    [JsonIgnore]
    public string HashAlgorithmName => Enum.GetName(typeof(HashAlgorithmType), HashAlgorithm) ?? "undefined";
    
    [JsonIgnore]
    private bool _isSelected = false;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            SetProperty(ref _isSelected, value);
            if (WhenSelected is not null)
                WhenSelected();
        }
    }

    [JsonIgnore]
    public Action? WhenSelected { get; set; }
}
