using Avalonia.Controls;
using Avalonia.Input;
using HashCheck.ViewModels;

namespace HashCheck.Views;

public partial class DragDropPopupControl : UserControl
{
    private DragDropPopupControlVM DataContextVM => (DragDropPopupControlVM)DataContext!;

    public DragDropPopupControl()
    {
        InitializeComponent(true);
        this.DataContext = new DragDropPopupControlVM() { View = this, WindowContentService = new WindowContentService() };

        this.AddHandler(DragDrop.DragLeaveEvent, (o, e) => { this.IsVisible = false; });
        this.AddHandler(DragDrop.DropEvent, (o, e) => { this.IsVisible = false; });

        Border NewBorder = (Border)(DDP_New.Parent as Grid)!.Children[0];
        Border CompareBorder = (Border)(DDP_Compare.Parent as Grid)!.Children[0];

        DDP_New.AddHandler(DragDrop.DragOverEvent, (o, e) =>
        {
            NewBorder.Classes.Add("dragover");
            DataContextVM.DragOverAccess(o, e);
        });

        DDP_New.AddHandler(DragDrop.DropEvent, (o, e) =>
        {
            this.IsVisible = false;
            NewBorder.Classes.Remove("dragover");
            DataContextVM.DropObjectForHash(o, e).Wait();
        });

        DDP_New.AddHandler(DragDrop.DragLeaveEvent, (o, e) => NewBorder.Classes.Remove("dragover"));

        DDP_Compare.AddHandler(DragDrop.DragOverEvent, (o, e) =>
        {
            CompareBorder.Classes.Add("dragover");
            DataContextVM.DragOverAccess(o, e);
        });

        DDP_Compare.AddHandler(DragDrop.DropEvent, (o, e) =>
        {
            this.IsVisible = false;
            CompareBorder.Classes.Remove("dragover");
            DataContextVM.DropObjectForComparing(o, e).Wait();
        });

        DDP_Compare.AddHandler(DragDrop.DragLeaveEvent, (o, e) => CompareBorder.Classes.Remove("dragover"));
    }
}
