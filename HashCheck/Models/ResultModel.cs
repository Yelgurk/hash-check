using System;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HashCheck.Models
{
    public partial class ResultModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FileName))]
        [NotifyPropertyChangedFor(nameof(FilePath))]
        private string? _fileFullPath;

        public string FileName => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Split('\\')[FileFullPath.Split('\\').Count() - 1];

        public string FilePath => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Substring(0, FileFullPath.LastIndexOf('\\'));

        [ObservableProperty]
        private List<CalculatedHashResult>? _fileHashes;
    }
}