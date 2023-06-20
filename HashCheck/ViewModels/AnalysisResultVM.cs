using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using HashCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneOf.Types;

namespace HashCheck.ViewModels;

public partial class AnalysisResultVM : VMBase
{
    public ObservableCollection<ResultModel> Results { get; set; } = new();

    [RelayCommand]
    async Task OpenFolderSelectFile(ResultModel file)
    {
        ExplorerProvider.OpenFolderAndSelectItem(file.FilePath, file.FileName);
    }

    [RelayCommand]
    async Task CopyHashToClipboard(string hash) => await this.View.ParentWindow()!.Clipboard!.SetTextAsync(hash);
}
