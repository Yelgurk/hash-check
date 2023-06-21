using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using HashCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Domain;

namespace HashCheck.ViewModels
{
    public partial class FilesComparingResultVM : VMBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsHashesEqual))]
        private ResultHashModel? _firstFile;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsHashesEqual))]
        private ResultHashModel? _secondFile;

        public bool IsHashesEqual => FirstFile is not null && SecondFile is not null
            && FirstFile.FileHashes.SequenceEqual(SecondFile.FileHashes);

        [RelayCommand]
        public async Task OpenFolderSelectFile(ResultHashModel file) => ExplorerProvider.OpenFolderAndSelectItem(file.FilePath, file.FileName);

        [RelayCommand]
        public async Task CopyHashToClipboard(string hash) => await this.View.ParentWindow()!.Clipboard!.SetTextAsync(hash);
    }
}
