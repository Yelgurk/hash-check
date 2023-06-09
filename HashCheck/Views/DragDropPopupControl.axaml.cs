using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HashCheck.ViewModels;

namespace HashCheck.Views
{
    public partial class DragDropPopupControl : UserControl
    {
        public DragDropPopupControl()
        {
            this.InitializeComponent();
            this.DataContext = new DragDropPopupControlVM() { View = this, WindowContentService = new WindowContentService() };

            this.AddHandler(DragDrop.DragLeaveEvent, (o, e) => { this.IsVisible = false; });
            this.FindControl<Border>("DragDropPlace_NewSingle").AddHandler(DragDrop.DragOverEvent, (DataContext as DragDropPopupControlVM)!.DragOverAccess);
            this.FindControl<Border>("DragDropPlace_NewCompare").AddHandler(DragDrop.DragOverEvent, (DataContext as DragDropPopupControlVM)!.DragOverAccess);
            this.FindControl<Border>("DragDropPlace_NewSingle").AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                (DataContext as DragDropPopupControlVM)!.DropObjectForHash(o, e);
            });
            this.FindControl<Border>("DragDropPlace_NewCompare").AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                (DataContext as DragDropPopupControlVM)!.DropObjectForComparing(o, e);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
