using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Gemstone.Collections.CollectionExtensions;
using OneOf;

namespace HashCheck.Domain;

[GenerateOneOf]
public sealed partial class FilePathsOrDirectoryPath : OneOfBase<List<string>, string>
{
    public bool IsFilePaths => this.IsT0;
    public bool IsDirectoryPath => this.IsT1;
    
    public List<string> AsFilePaths => this.AsT0;
    public string AsDirectoryPath => this.AsT1;
}

[GenerateOneOf]
public sealed partial class ComputeHashResultOr<T> : OneOfBase<T, (byte[] Hash, HashAlgorithmType HashAlgorithm)>
{
    public bool IsError => this.IsT0;
    public bool IsHash => this.IsT1;
    
    public T AsError => this.AsT0;
    public (byte[] Hash, HashAlgorithmType HashAlgorithm) AsHash => this.AsT1;
}

public static class HashApi
{
    public static async Task<byte[]> ComputeHashAsync(
        Stream dataStream, HashAlgorithmType hashAlgorithmType, CancellationToken ct = default)
    {
        using HashAlgorithm hashAlgorithm = hashAlgorithmType switch
        {
            HashAlgorithmType.MD5 => MD5.Create(),
            HashAlgorithmType.SHA1 => SHA1.Create(),
            HashAlgorithmType.SHA256 => SHA256.Create(),
            HashAlgorithmType.SHA384 => SHA384.Create(),
            HashAlgorithmType.SHA512 or _ => SHA512.Create(),
        };

        return await hashAlgorithm.ComputeHashAsync(dataStream, ct);
    }

    public static async Task<ComputeHashResultOr<FileNotExists>> ComputeHashAsync(
        string filePath, HashAlgorithmType hashAlgorithmType, CancellationToken ct = default)
    {
        if (!File.Exists(filePath))
        {
            return new FileNotExists(filePath);
        }
        
        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);

        return (await ComputeHashAsync(fileStream, hashAlgorithmType, ct), hashAlgorithmType);
    }

    public static async IAsyncEnumerable<(string FilePath, ComputeHashResultOr<FileNotExists> Hashes)>
        ComputeHashesAsync(FilePathsOrDirectoryPath where, HashAlgorithmType hashAlgorithmType, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var filesPaths = where.Match(
            filesPaths => filesPaths,
            directory => Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
        );

        foreach (var filePath in filesPaths)
        {
            yield return (filePath, await ComputeHashAsync(filePath, hashAlgorithmType, ct));
        }
    }

    public static async IAsyncEnumerable<ComputeHashResultOr<FileNotExists>> ComputeHashesAsync(
        string filePath, IEnumerable<HashAlgorithmType> hashAlgorithmTypes,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        foreach (var hashAlgorithmType in hashAlgorithmTypes)
        {
            yield return await ComputeHashAsync(filePath, hashAlgorithmType, ct);
        }
    }
    
    public static async IAsyncEnumerable<(string FilePath, List<ComputeHashResultOr<FileNotExists>> Hashes)> 
        ComputeHashesAsync(FilePathsOrDirectoryPath where, IEnumerable<HashAlgorithmType> hashAlgorithmTypes,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        var hashAlgorithmTypesList = hashAlgorithmTypes.ToList();
        var filesPaths = where.Match(
            filesPaths => filesPaths,
            directory => Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
        );
        
        foreach (var filesPath in filesPaths)
        {
            var computedHashesList = await ComputeHashesAsync(filesPath, hashAlgorithmTypesList, ct).ToListAsync(ct);
            yield return (filesPath, computedHashesList);
        }
    }

    /*
    public static IEnumerable<object> ComputeHashesAsync(FilePathsOrDirectoryPath where, IEnumerable<System.Security.Authentication.HashAlgorithmType> hashAlgorithmTypes)
    {
        throw new NotImplementedException();
    }
    */
}