using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.Models;

public partial class HashModel : ObservableObject
{
    [ObservableProperty]
    private string hashName;

    [ObservableProperty]
    private Func<string, string> hashMethod;
}