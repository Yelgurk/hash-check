using Avalonia.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumFastToStringGenerated;
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

            var hashAlgorithms = App.Host!.Services.GetRequiredService<SettingFile>()
                .Hashes
                .Where(h => h.IsSelected)
                .Select(h => Enum.Parse<HashAlgorithmType>(h.HashName!))
                .ToList();

            await foreach (var (file, hashes) in HashApi.ComputeHashesAsync(new FilePathsOrDirectoryPath(files), hashAlgorithms))
            {
                var onlyComputedHashes = hashes
                    .Where(cr => cr.IsHash)
                    .Select(cr => cr.AsHash)
                    .ToList();

                if (onlyComputedHashes.Count is not 0)
                {
                    var calculatedHashResults = onlyComputedHashes
                        .Select(h => new CalculatedHashResult
                        {
                            HashAlgorithmType = h.HashAlgorithm,
                            HashValue = h.Hash
                        })
                        .ToList();
                    
                    analysisResultVm.Results.Add(new ResultModel
                    {
                        FileHashes = calculatedHashResults,
                        FileFullPath = file
                    });
                }
            }
        }

        public async Task DropObjectForComparing(object sender, DragEventArgs e)
        {
            if (!e.Data.Contains(DataFormats.Files))
            {
                return;
                // App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(, true);
            }
            
            var files = e.Data.GetFileNames()!.ToArray();

        }
    }
}
