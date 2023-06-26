using System;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using OneOf.Types;

namespace HashCheck.Models;

public partial class ResultHashModel : ObservableObject
{
    [NotifyPropertyChangedFor(nameof(FileName))]
    [NotifyPropertyChangedFor(nameof(FilePath))]
    [ObservableProperty]
    private string fileFullPath = string.Empty;

    public string FileName => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Split('\\')[FileFullPath.Split('\\').Count() - 1];

    public string FilePath => string.IsNullOrEmpty(FileFullPath) ? "" : FileFullPath!.Substring(0, FileFullPath.LastIndexOf('\\'));

    [ObservableProperty]
    private List<CalculatedHashResult> fileHashes = new();

    public override bool Equals(object? obj) => obj is null ? false : this.FileHashes.SequenceEqual((obj as ResultHashModel)!.FileHashes);

    //public bool IsEqual(ResultHashModel? Right) => Right is not null ? this.FileHashes!.Equals(Right.FileHashes) : false;
}