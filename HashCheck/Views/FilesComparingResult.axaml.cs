using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class FilesComparingResult : UserControl
    {
        public FilesComparingResult() : this(new WindowContentService())
        { }

        public FilesComparingResult(IWindowContentService _windowContentService)
        {
            InitializeComponent(true);
            this.DataContext = new FilesComparingResultVM() { View = this, WindowContentService = _windowContentService };

            this.AddHandler(DragDrop.DragEnterEvent, (o, e) => { this.FindControl<UserControl>("DragDropPlace_ChoiceContainer").IsVisible = true; });
        }
    }
}
