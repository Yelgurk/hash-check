using Avalonia.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumFastToStringGenerated;
using Gemstone.Collections.CollectionExtensions;
using HashCheck.Domain;
using HashCheck.Models;
using HashCheck.Views;

namespace HashCheck.ViewModels
{
    public partial class DragDropPopupControlVM : VMBase
    {
        public void DragOverAccess(object sender, DragEventArgs e)
        {
            e.DragEffects &= DragDropEffects.Link;
            if (!e.Data.Contains(DataFormats.Files))
                e.DragEffects = DragDropEffects.None;
        }

        public async Task DropObjectForHash(object sender, DragEventArgs e)
        {
            if (!e.Data.Contains(DataFormats.Files))
            {
                return;
            }
            
            var files = e.Data.GetFileNames()!.ToList();
            var analysisResultVm = (WindowContentService.Set<AnalysisResult>().DataContext as AnalysisResultVM)!;

            var resultHashModels = await App.Host!.Services
                .GetRequiredService<HashComputeService>()
                .ComputeHashesFor(new FilePathsOrDirectoryPath(files));
            
            analysisResultVm.Results.AddRange(resultHashModels);
        }

        public async Task DropObjectForComparing(object sender, DragEventArgs e)
        {
            if (!e.Data.Contains(DataFormats.Files))
            {
                return;
                // App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(, true);
            }
            
            var files = e.Data.GetFileNames()!.ToList();
            var resultHashModels = await App.Host!.Services
                .GetRequiredService<HashComputeService>()
                .ComputeHashesFor(new FilePathsOrDirectoryPath(files));

            VMBase? vm = resultHashModels.Count switch
            {
                <= 0 => null, // nothing
                1 or > 2 => (WindowContentService.Set<AnalysisResult>().DataContext as AnalysisResultVM)!,
                2 => (WindowContentService.Set<FilesComparingResult>().DataContext as FilesComparingResultVM)!,
            };
            
            if (vm is AnalysisResultVM analysisResultVm)
            {
                analysisResultVm.Results.AddRange(resultHashModels);
            }
            else if (vm is FilesComparingResultVM filesComparingResultVm)
            {
                filesComparingResultVm.FirstFile = resultHashModels[0];
                filesComparingResultVm.SecondFile = resultHashModels[1];
            }
        }
    }
}
