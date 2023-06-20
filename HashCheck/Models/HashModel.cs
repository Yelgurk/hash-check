using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace HashCheck.Models;

public partial class HashModel : ObservableObject
{
    [ObservableProperty]
    private string? _hashName;

    private bool _isSelected;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (SetProperty(ref _isSelected, value))
                App.Host!.Services.GetRequiredService<SettingFile>()!
                    .SaveSettings(SettingsVM.SettingPath);
        }
    }

    public bool LoadSelectState { set => SetProperty(ref _isSelected, value); }
}