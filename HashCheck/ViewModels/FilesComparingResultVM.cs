using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.ViewModels
{
    public class FilesComparingResultVM : VMBase
    {
        public HashComputator Computator { get; init; }

        public FilesComparingResultVM() => this.Computator = App.Host!.Services.GetRequiredService<HashComputator>();
    }
}
