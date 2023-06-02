using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Models;
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
    private HashModel? _selectedHash;
    public HashModel SelectedHash
    {
        get => _selectedHash ?? new HashModel() { HashName = "init" };
        set
        {
            SetProperty(ref _selectedHash, value);
            Calculate().ContinueWith(awaiter => { });
        }
    }

    public ObservableCollection<HashModel> Hashes { get; } = new ObservableCollection<HashModel>();

    public List<string> FilePaths { get; set; } = new List<string>();

    public ObservableCollection<ResultModel> Result { get; } = new ObservableCollection<ResultModel>();

    public HashComputator()
    {
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
        await Task.Run(() => FilePaths.ForEach(path => Result.Add(new ResultModel() { FileName = path, FilePath = path, FileHash = SelectedHash.HashMethod(path) })));
    }

    /*
    private string ComputeHash<T>(string filePath) where T : HashAlgorithm
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
            return BitConverter.ToString(T.Create().ComputeHash(fs)).Replace("-", "");
    }
    */
}
