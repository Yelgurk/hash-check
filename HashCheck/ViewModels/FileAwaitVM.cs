using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
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
        public async Task ShowMessage(string msg, decimal centenary, double year)
        {
            await Task.Delay(0);
            Debug.WriteLine(msg + centenary + year);
        }

        public async Task DropFileAndDir(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
                await PathTreeParser(e.Data.GetFileNames()!.ToArray());
        }

        public async Task DragOverAccess(object sender, DragEventArgs e)
        {
            e.DragEffects = e.DragEffects & DragDropEffects.Link;
            if (!e.Data.Contains(DataFormats.FileNames))
                e.DragEffects = DragDropEffects.None;
        }

        [RelayCommand]
        async Task PeekFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { AllowMultiple = true };

            string[]? paths = await openFileDialog.ShowAsync(this.View.ParentWindow()!);

            if (paths is not null)
                PathTreeParser(paths);
        }

        [RelayCommand]
        async Task PeekFolder()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();

            string? path = await openFolderDialog.ShowAsync(this.View.ParentWindow()!);

            if (path is not null && Directory.Exists(path))
                PathTreeParser(new[] { path });
        }

        public async Task PathTreeParser(string[] headers)
        {
            List<string> forAnalyze = headers
                    .SelectMany(p =>
                    {
                        if (File.Exists(p))
                            return new[] { p };
                        else if (Directory.Exists(p))
                            return Directory.EnumerateFiles(p, "*", SearchOption.AllDirectories);
                        else
                            return Enumerable.Empty<string>();
                    }).ToList();

            if (forAnalyze.Count == 1)
                WindowContentService.Set<SingleAnalysisResult>();
            else if (forAnalyze.Count > 1)
                WindowContentService.Set<MultiAnalysisResult>();

            App.Host!.Services.GetRequiredService<HashComputator>()!.Calculate(forAnalyze);
        }
    }
}
