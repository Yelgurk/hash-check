using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.Models;

public class SettingsModel
{
    public List<SelectableHashModel>? SelectableHashModels { get; set; }

    public SelectableThemeModel? ThemeModel { get; set; }
}
