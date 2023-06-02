using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.ViewModels
{
    public class MultiAnalysisResultVM : VMBase
    {
        public HashComputator Computator { get; init; }

        public MultiAnalysisResultVM() => this.Computator = App.Host!.Services.GetRequiredService<HashComputator>();
    }
}
