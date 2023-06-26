using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Domain;
using HashCheck.Models;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace HashCheck.ViewModels;

public class SettingsVM : VMBase
{
    private ThemeModel? _appSelectedTheme = null;

    public ThemeModel AppSelectedTheme
    {
        get => _appSelectedTheme ?? AllExistedThemes[0];
        set
        {
            if (_appSelectedTheme is null)
                SetProperty(ref _appSelectedTheme, value);
            else
            {
                SetProperty(ref _appSelectedTheme, value);
                SettingsService.SaveSettingIntoJSON(AllExistedHashes.ToList(), AppSelectedTheme);
            }
            
            SetStyles(value);
        }
    } 

    public ObservableCollection<ThemeModel> AllExistedThemes { get; } = new ObservableCollection<ThemeModel>()
    {
        new ThemeModel()
        {
            Name = "Light",
            Resource = ThemeVariant.Light,
            WindowBackground = ThemeModel.AcrylicBorderGenerator(Brushes.WhiteSmoke.Color),
            ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemePrimary"]! },
            ColorBase = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["LightThemeBase"]! },
            IsTransparent = true
        },
        new ThemeModel()
        {
            Name = "Dark",
            Resource = ThemeVariant.Dark,
            WindowBackground = ThemeModel.AcrylicBorderGenerator(Brushes.Black.Color),
            ColorPrimary = new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemePrimary"]! },
            ColorBase =  new SolidColorBrush() { Color = (Color)Application.Current!.Resources["DarkThemeBase"]! },
            IsTransparent = true
        }
    };

    public ObservableCollection<SelectableHashModel> AllExistedHashes { get; } = new ObservableCollection<SelectableHashModel>(
            Enum.GetValues<HashAlgorithmType>()
                .Select(h => new SelectableHashModel() {
                    HashAlgorithm = h,
                    IsSelected = false
                })
        );

    public SettingsVM()
    {
        if (!SettingsService.LoadSettingsFromJSON())
            SettingsService.SaveSettingIntoJSON(AllExistedHashes.ToList(), AppSelectedTheme);

        this.AppSelectedTheme = AllExistedThemes.Single(t => t.Index == SettingsService.SettingsFile!.ThemeModel!.Index);
        this.AppSelectedTheme.IsTransparent = SettingsService.SettingsFile!.ThemeModel!.IsTransparent;

        this.AllExistedHashes
            .Where(h => h.IsSelected != SettingsService.SettingsFile!.SelectableHashModels!.Single(x => x.HashAlgorithmName == h.HashAlgorithmName).IsSelected)
            .Do(h => { foreach (SelectableHashModel LoadedHashesInfo in h) LoadedHashesInfo.IsSelected = !LoadedHashesInfo.IsSelected; });

        foreach (SelectableHashModel SelectableHash in AllExistedHashes)
            SelectableHash.WhenSelected = () => SettingsService.SaveSettingIntoJSON(AllExistedHashes.ToList(), AppSelectedTheme);

        foreach (ThemeModel SelectableStyle in AllExistedThemes)
            SelectableStyle.WhenSelected = () => SettingsService.SaveSettingIntoJSON(AllExistedHashes.ToList(), AppSelectedTheme);
    }

    private void SetStyles(ThemeModel Style)
    {
        App.Host!.Services.GetRequiredService<MainWindow>()!.SetBackground(Style.WindowBackground);
        App.SetTheme(Style.Resource);
    }
}