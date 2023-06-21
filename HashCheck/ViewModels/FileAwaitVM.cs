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
using Gemstone.Collections.CollectionExtensions;
using HashCheck.Domain;

namespace HashCheck.ViewModels
{
    public partial class FileAwaitVM : VMBase
    {
        [RelayCommand]
        public async Task PeekFile()
        {
            OpenFileDialog openFileDialog = new() { AllowMultiple = true };

            var paths = await openFileDialog.ShowAsync(View.ParentWindow()!) ?? Array.Empty<string>();

            var resultHashModels = await App.Host!.Services
                .GetRequiredService<HashComputeService>()
                .ComputeHashesFor(new FilePathsOrDirectoryPath(paths.ToList()));

            App.Host.Services
                .GetRequiredService<WindowContentService>()
                .Set<AnalysisResult>()
                .DataContext?.Cast<AnalysisResultVM>()
                ?.Results.AddRange(resultHashModels);
        }

        [RelayCommand]
        public async Task PeekFolder()
        {
            OpenFolderDialog openFolderDialog = new();

            var path = await openFolderDialog.ShowAsync(View.ParentWindow()!);

            if (path is not null && Directory.Exists(path))
            {
                var resultHashModels = await App.Host!.Services
                    .GetRequiredService<HashComputeService>()
                    .ComputeHashesFor(new FilePathsOrDirectoryPath(path));

                App.Host.Services
                    .GetRequiredService<WindowContentService>()
                    .Set<AnalysisResult>()
                    .DataContext?.Cast<AnalysisResultVM>()
                    ?.Results.AddRange(resultHashModels);
            }
        }

        [RelayCommand]
        public async Task PeekFilesForCompare()
        {
            OpenFileDialog openFileDialog = new() { AllowMultiple = true };

            var paths = await openFileDialog.ShowAsync(View.ParentWindow()!) ?? Array.Empty<string>();

            if (paths.Length is 2)
            {
                var resultHashModels = await App.Host!.Services
                    .GetRequiredService<HashComputeService>()
                    .ComputeHashesFor(new FilePathsOrDirectoryPath(paths.ToList()));

                App.Host.Services
                    .GetRequiredService<WindowContentService>()
                    .Set<FilesComparingResult>()
                    .DataContext?.Cast<FilesComparingResultVM>()
                    ?.Do(vm => vm.FirstFile = resultHashModels[0])
                    ?.Do(vm => vm.SecondFile = resultHashModels[1]);
            }
            else
            {
                // todo: incorrect files count for compare
            }
        }

        [RelayCommand]
        public void GoToSettings() => (WindowContentService as IWindowContentService).Set<Settings>();
    }
}
