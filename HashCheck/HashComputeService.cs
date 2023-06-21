using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashCheck.Domain;
using HashCheck.Models;

namespace HashCheck;

public sealed class HashComputeService
{
    private readonly SettingFile _settingFile;
    
    public HashComputeService(SettingFile settingFile)
    {
        this._settingFile = settingFile;
    }

    public async Task<List<ResultHashModel>> ComputeHashesFor(FilePathsOrDirectoryPath where)
    {
        var hashAlgorithms = _settingFile
            .Hashes
            .Where(h => h.IsSelected)
            .Select(h => Enum.Parse<HashAlgorithmType>(h.HashName!))
            .ToList();

        return await HashApi.ComputeHashesAsync(where, hashAlgorithms)
            .Select(fileWithHashes =>
            {
                var onlyComputedHashes = fileWithHashes.Hashes
                    .Where(cr => cr.IsHash)
                    .Select(cr => cr.AsHash)
                    .ToList();

                return (fileWithHashes.FilePath, onlyComputedHashes);
            })
            .Where(x => x.onlyComputedHashes.Count is not 0)
            .Select(x =>
            {
                var calculatedHashResults = x.onlyComputedHashes
                    .Select(h => new CalculatedHashResult
                    {
                        HashAlgorithmType = h.HashAlgorithm,
                        HashValue = h.Hash
                    })
                    .ToList();

                return new ResultHashModel
                {
                    FileHashes = calculatedHashResults,
                    FileFullPath = x.FilePath
                };
            })
            .ToListAsync();
    }
}