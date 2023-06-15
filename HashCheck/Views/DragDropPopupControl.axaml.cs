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
            InitializeComponent(true);
            this.DataContext = new DragDropPopupControlVM() { View = this, WindowContentService = new WindowContentService() };

            this.AddHandler(DragDrop.DragLeaveEvent, (o, e) => { this.IsVisible = false; });
            this.AddHandler(DragDrop.DropEvent, (o, e) => { this.IsVisible = false; });

            DragDropPlace_NewSingle.AddHandler(DragDrop.DragOverEvent, (o, e) => {
                DragDropPlace_NewSingle.Child!.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DragOverAccess(o, e);
            });

            DragDropPlace_NewSingle.AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                DragDropPlace_NewSingle.Child!.Classes.Remove("dragover");
                (DataContext as DragDropPopupControlVM)!.DropObjectForHash(o, e).Wait();
            });

            DragDropPlace_NewSingle.AddHandler(DragDrop.DragLeaveEvent, (o, e) => {
                DragDropPlace_NewSingle.Child!.Classes.Remove("dragover");
            });

            DragDropPlace_NewCompare.AddHandler(DragDrop.DragOverEvent, (o, e) => {
                DragDropPlace_NewCompare.Child!.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DragOverAccess(o, e);
            });

            DragDropPlace_NewCompare.AddHandler(DragDrop.DropEvent, (o, e) => {
                this.IsVisible = false;
                DragDropPlace_NewCompare.Child!.Classes.Add("dragover");
                (DataContext as DragDropPopupControlVM)!.DropObjectForComparing(o, e).Wait();
            });

            DragDropPlace_NewCompare.AddHandler(DragDrop.DragLeaveEvent, (o, e) => {
                DragDropPlace_NewCompare.Child!.Classes.Remove("dragover");
            });
        }
    }
}
