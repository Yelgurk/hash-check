using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace HashCheck.ViewModels;

public partial class SingleAnalysisResultVM : VMBase
{
    public HashComputator Computator { get; init; }

    public SingleAnalysisResultVM() => this.Computator = App.Host!.Services.GetRequiredService<HashComputator>();
}