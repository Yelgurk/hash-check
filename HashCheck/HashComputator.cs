using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Models;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck;

public partial class HashComputator : ObservableObject
{
    IWindowContentService WindowContentService;

    private HashModel? _selectedHash;
    public HashModel SelectedHash
    {
        get => _selectedHash ?? new HashModel() { HashName = "init", HashMethod = (none) => { return ""; } };
        set
        {
            SetProperty(ref _selectedHash, value);
            Calculate().ContinueWith(awaiter => { });
        }
    }

    public ObservableCollection<HashModel> Hashes { get; } = new ObservableCollection<HashModel>();

    public List<string> FilePaths { get; set; } = new List<string>();

    public ObservableCollection<ResultModel> Result { get; } = new ObservableCollection<ResultModel>();

    public bool IsHashesEqual => Result.Count != 2 ? false : Result[0] == Result[1];

    public HashComputator(IWindowContentService _windowContentService)
    {
        this.WindowContentService = _windowContentService;

        Hashes.Add(new HashModel()
        {
            HashName = "MD5",
            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(MD5.Create().ComputeHash(fs)); }
        });

        Hashes.Add(new HashModel()
        {
            HashName = "SHA1",
            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA1.Create().ComputeHash(fs)); }
        });

        Hashes.Add(new HashModel()
        {
            HashName = "SHA256",
            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA256.Create().ComputeHash(fs)); }
        });

        Hashes.Add(new HashModel()
        {
            HashName = "SHA384",
            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA384.Create().ComputeHash(fs)); }
        });

        Hashes.Add(new HashModel()
        {
            HashName = "SHA512",
            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA512.Create().ComputeHash(fs)); }
        });

        SelectedHash = Hashes[0];
    }

    private string HashToString(byte[] HashCode) { return BitConverter.ToString(HashCode).Replace("-", ""); }

    public async Task Calculate(List<string> FilePaths)
    {
        this.FilePaths = FilePaths;
        await Calculate();
    }

    public async Task Calculate()
    {
        Result.Clear();
        await Task.Run(() =>
            FilePaths.ForEach(path => {
                Result.Add(new ResultModel() {
                    FileFullPath = path,
                    FileHash = Hashes.Where((hash) => hash.IsSelected).Select<HashModel, string>((selected) => selected.HashMethod(path)).DefaultIfEmpty("hash not selected").ToList()!
                });
            })
        );

        OnPropertyChanged(nameof(IsHashesEqual));
    }

    public async Task PathTreeParser(string[] headers, bool comparing = false)
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

        if (comparing && forAnalyze.Count > 0 && Result.Count > 0)
        {
            forAnalyze.RemoveRange(1, forAnalyze.Count - 1);
            forAnalyze.Add(Result[0].FileFullPath!);
            forAnalyze.Reverse();
            WindowContentService.Set<FilesComparingResult>();
        }
        else if (forAnalyze.Count == 1)
            WindowContentService.Set<SingleAnalysisResult>();
        else if (forAnalyze.Count > 1)
            WindowContentService.Set<MultiAnalysisResult>();

        Calculate(forAnalyze);
    }
}