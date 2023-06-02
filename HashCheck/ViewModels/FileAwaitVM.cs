using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
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
        async Task PeekFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { AllowMultiple = true };

            string[]? paths = await openFileDialog.ShowAsync(this.View.ParentWindow()!);

            if (paths is not null)
            {
                List<string> for_analyze = paths
                    .SelectMany(p =>
                    {
                        if (File.Exists(p))
                            return new[] { p };
                        else if (Directory.Exists(p))
                            return Directory.EnumerateFiles(p, "*", SearchOption.AllDirectories);
                        else
                            return Enumerable.Empty<string>();
                    }).ToList();

                if (for_analyze.Count == 1)
                    WindowContentService.Set<SingleAnalysisResult>();
                else if (for_analyze.Count > 1)
                    WindowContentService.Set<MultiAnalysisResult>();

                App.Host!.Services.GetRequiredService<HashComputator>()!.Calculate(for_analyze);
            }
        }

        [RelayCommand]
        async Task PeekFolder()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();

            string? path = await openFolderDialog.ShowAsync(this.View.ParentWindow()!);

            if (path is not null && Directory.Exists(path))
            {
                List<string> for_analyze = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).ToList();

                if (for_analyze.Count == 1)
                    WindowContentService.Set<SingleAnalysisResult>();
                else if (for_analyze.Count > 1)
                    WindowContentService.Set<MultiAnalysisResult>();

                App.Host!.Services.GetRequiredService<HashComputator>()!.Calculate(for_analyze);
            }
        }
    }
}
