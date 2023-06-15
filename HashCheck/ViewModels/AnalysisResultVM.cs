using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using HashCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.ViewModels;

public partial class AnalysisResultVM : VMBase
{
    public HashComputator Computator { get; init; }

    public AnalysisResultVM() => this.Computator = App.Host!.Services.GetRequiredService<HashComputator>();

    [RelayCommand]
    async Task OpenFolderSelectFile(ResultModel file) => ExplorerProvider.OpenFolderAndSelectItem(file.FilePath, file.FileName);

    [RelayCommand]
    async Task CopyHashToClipboard(string hash) => await this.View.ParentWindow()!.Clipboard!.SetTextAsync(hash);
}
