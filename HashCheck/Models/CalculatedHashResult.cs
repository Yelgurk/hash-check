using System;
using CommunityToolkit.Mvvm.ComponentModel;
using EnumFastToStringGenerated;
using HashCheck.Domain;

namespace HashCheck.Models;

public partial class CalculatedHashResult : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HashAlgorithmTypeAsString))]
    private HashAlgorithmType _hashAlgorithmType;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HashValueAsString))]
    private byte[]? _hashValue;
    
    public string HashValueAsString => BitConverter.ToString(this.HashValue!).Replace("-","");
    
    public string HashAlgorithmTypeAsString => HashAlgorithmType.ToStringFast();
}