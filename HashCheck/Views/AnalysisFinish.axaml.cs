using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HashCheck.Views;

public partial class AnalysisFinish : UserControl
{
    public AnalysisFinish()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}