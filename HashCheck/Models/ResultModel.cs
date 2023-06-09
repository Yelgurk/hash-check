using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.Models
{
    public partial class ResultModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FileName))]
        [NotifyPropertyChangedFor(nameof(FilePath))]
        private string? fileFullPath;

        public string FileName => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Split('\\')[FileFullPath.Split('\\').Count() - 1];

        public string FilePath => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Substring(0, FileFullPath.LastIndexOf('\\'));

        [ObservableProperty]
        private string? fileHash;

        public static bool operator ==(ResultModel Result1, ResultModel Result2) => Result1.FileHash == Result2.FileHash;
        public static bool operator !=(ResultModel Result1, ResultModel Result2) => Result1.FileHash != Result2.FileHash;
    }
}
