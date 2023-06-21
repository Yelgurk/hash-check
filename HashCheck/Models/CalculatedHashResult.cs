using System;
using CommunityToolkit.Mvvm.ComponentModel;
using EnumFastToStringGenerated;
using Generator.Equals;
using HashCheck.Domain;

namespace HashCheck.Models;

[Equatable]
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