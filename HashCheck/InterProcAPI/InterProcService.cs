using System.IO;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using Gemstone.Collections.CollectionExtensions;
using HashCheck.Domain;
using HashCheck.ViewModels;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HashCheck.InterProcAPI;

public class InterProcService : HashCheckLocalhost.HashCheckLocalhostBase
{
    public override Task<Empty> LocalObjectHashCalc(HashCheckRequest request, ServerCallContext context)
    {
        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            var forAnalyze = request.ObjectURI.ToArray()
                .SelectMany(p =>
                {
                    if (File.Exists(p))
                        return new[] { p };
                    if (Directory.Exists(p))
                        return Directory.EnumerateFiles(p, "*", SearchOption.AllDirectories);
                    return Enumerable.Empty<string>();
                }).ToList();

            var resultHashModels = await App.Host!.Services
                .GetRequiredService<HashComputeService>()
                .ComputeHashesFor(new FilePathsOrDirectoryPath(forAnalyze));

            App.Host.Services
                .GetRequiredService<WindowContentService>()
                .Set<AnalysisResult>()
                .DataContext?.Cast<AnalysisResultVM>()
                ?.Results.AddRange(resultHashModels);
        });
        
        return Task.FromResult(new Empty());
    }
}