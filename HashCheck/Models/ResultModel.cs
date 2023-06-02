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
        private string _fileName;

        public string FileName
        {
            get => string.IsNullOrEmpty(_fileName) ? "" : _fileName;
            set => SetProperty(ref _fileName, value!.Split('\\')[value.Split('\\').Count() - 1]);
        }

        private string _filePath;

        public string FilePath
        {
            get => string.IsNullOrEmpty(_filePath) ? "" : _filePath;
            set => SetProperty(ref _filePath, value!.Substring(0, value.LastIndexOf('\\')));
        }

        [ObservableProperty]
        private string fileHash;
    }
}
