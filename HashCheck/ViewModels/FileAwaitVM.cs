using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HashCheck.ViewModels
{
    public partial class FileAwaitVM : VMBase
    {
        [RelayCommand]
        public async Task PeekFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { AllowMultiple = true };

            string[]? paths = await openFileDialog.ShowAsync(this.View.ParentWindow()!);

            if (paths is not null)
                App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(paths);
        }

        [RelayCommand]
        public async Task PeekFolder()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();

            string? path = await openFolderDialog.ShowAsync(this.View.ParentWindow()!);

            if (path is not null && Directory.Exists(path))
                App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(new[] { path });
        }

        [RelayCommand]
        public void GoToSettings() => WindowContentService.Set<Settings>();
    }
}
