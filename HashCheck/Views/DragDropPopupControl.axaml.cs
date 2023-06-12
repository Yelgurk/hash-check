using Avalonia;
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
            this.AddHandler(DragDrop.DropEvent, (o, e) => { this.IsVisible = false; });

            Border DDSingle = this.FindControl<Border>("DragDropPlace_NewSingle");
            Border DDCompare = this.FindControl<Border>("DragDropPlace_NewCompare");

            DDSingle.AddHandler(DragDrop.DragOverEvent, (o, e) => {
                DDSingle.Child.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DragOverAccess(o, e);
            });

            DDSingle.AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                DDSingle.Child.Classes.Remove("dragover");
                (DataContext as DragDropPopupControlVM)!.DropObjectForHash(o, e);
            });

            DDSingle.AddHandler(DragDrop.DragLeaveEvent, (o, e) => {
                DDSingle.Child.Classes.Remove("dragover");
            });

            DDCompare.AddHandler(DragDrop.DragOverEvent, (o, e) => {
                DDCompare.Child.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DragOverAccess(o, e);
            });

            DDCompare.AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                DDCompare.Child.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DropObjectForComparing(o, e);
            });

            DDCompare.AddHandler(DragDrop.DragLeaveEvent, (o, e) => {
                DDCompare.Child.Classes.Remove("dragover");
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
