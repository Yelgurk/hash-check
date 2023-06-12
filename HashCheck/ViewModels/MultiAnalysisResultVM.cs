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

public partial class MultiAnalysisResultVM : VMBase
{
    public HashComputator Computator { get; init; }

    public MultiAnalysisResultVM() => this.Computator = App.Host!.Services.GetRequiredService<HashComputator>();

    [RelayCommand]
    async Task OpenFolderSelectFile(ResultModel file) => App.OpenFolderAndSelectItem(file.FilePath, file.FileName);

    [RelayCommand]
    async Task CopyHashToClipboard(string hash) => await Application.Current!.Clipboard!.SetTextAsync(hash);
}
