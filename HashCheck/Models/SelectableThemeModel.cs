using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HashCheck.Models;

public class SelectableThemeModel : ObservableObject
{
    public int Index { get; set; } = 0;
    public virtual bool IsTransparent { get; set; } = false;
}
