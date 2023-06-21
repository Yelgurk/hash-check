using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Models;
using HashCheck.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HashCheck;

//[Obsolete]
//public partial class HashComputator : ObservableObject
//{
//    IWindowContentService WindowContentService;
//
//    public ObservableCollection<HashModel> Hashes { get; } = new ObservableCollection<HashModel>();
//
//    public List<string> FilePaths { get; set; } = new List<string>();
//
//    public ObservableCollection<ResultHashModel> Result { get; } = new ObservableCollection<ResultHashModel>();
//
//    public bool IsHashesEqual => Result.Count != 2 ? false : Result[0] == Result[1];
//
//    public HashComputator(IWindowContentService _windowContentService)
//    {
//        this.WindowContentService = _windowContentService;
//
//        Hashes.Add(new HashModel()
//        {
//            HashName = "MD5",
//            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(MD5.Create().ComputeHash(fs)); }
//        });
//
//        Hashes.Add(new HashModel()
//        {
//            HashName = "SHA1",
//            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA1.Create().ComputeHash(fs)); }
//        });
//
//        Hashes.Add(new HashModel()
//        {
//            HashName = "SHA256",
//            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA256.Create().ComputeHash(fs)); }
//        });
//
//        Hashes.Add(new HashModel()
//        {
//            HashName = "SHA384",
//            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA384.Create().ComputeHash(fs)); }
//        });
//
//        Hashes.Add(new HashModel()
//        {
//            HashName = "SHA512",
//            HashMethod = (filePath) => { using (FileStream fs = new FileStream(filePath, FileMode.Open)) return HashToString(SHA512.Create().ComputeHash(fs)); }
//        });      
//    }
//
//    private string HashToString(byte[] HashCode) { return BitConverter.ToString(HashCode).Replace("-", ""); }
//
//    public async Task Calculate(List<string> FilePaths)
//    {
//        this.FilePaths = FilePaths;
//        await Calculate();
//    }
//
//    public async Task Calculate()
//    {
//        Result.Clear();
//        await Task.Run(() =>
//            FilePaths.ForEach(path => {
//                Result.Add(new ResultHashModel() {
//                    FileFullPath = path,
//                    FileHash = new List<HashModel>(
//                        Hashes.Where((hash) => hash.IsSelected)
//                            .Select<HashModel, HashModel>((calc) => new HashModel() { HashName = calc.HashName, HashValue = calc.HashMethod(path) })
//                                .DefaultIfEmpty(new HashModel() { HashName = "none", HashValue = "hash not selected" })
//                                    .ToList())
//                });
//            })
//        );
//
//        OnPropertyChanged(nameof(IsHashesEqual));
//    }
//
//    public async Task PathTreeParser(string[] headers, bool comparing = false)
//    {
//        List<string> forAnalyze = headers
//                .SelectMany(p =>
//                {
//                    if (File.Exists(p))
//                        return new[] { p };
//                    else if (Directory.Exists(p))
//                        return Directory.EnumerateFiles(p, "*", SearchOption.AllDirectories);
//                    else
//                        return Enumerable.Empty<string>();
//                }).ToList();
//
//        if (comparing && forAnalyze.Count > 1)
//        {
//            forAnalyze.RemoveRange(2, forAnalyze.Count - 2);
//            WindowContentService.Set<FilesComparingResult>();
//        }
//        else if (comparing && Result.Count > 0 && forAnalyze.Count > 0)
//        {
//            forAnalyze.RemoveRange(1, forAnalyze.Count - 1);
//            forAnalyze.Add(Result[0].FileFullPath!);
//            forAnalyze.Reverse();
//            WindowContentService.Set<FilesComparingResult>();
//        }
//        else if (forAnalyze.Count > 0)
//            WindowContentService.Set<AnalysisResult>();
//
//        Calculate(forAnalyze);
//    }
//}