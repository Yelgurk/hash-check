using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using HashCheck.Domain;
using HashCheck.Models;

namespace HashCheck;

public sealed class HashComputeService
{

    public async Task<List<ResultHashModel>> ComputeHashesFor(FilePathsOrDirectoryPath where)
    {
        if (SettingsService.SettingsFile == null)
            if (SettingsService.LoadSettingsFromJSON(SettingsService.DefaultSettingFilePath)!)
                return new List<ResultHashModel>();

        var hashAlgorithms =
            SettingsService.SettingsFile!
            .SelectableHashModels!
            .Where(h => h.IsSelected)
            .Select(h => h.HashAlgorithm);

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