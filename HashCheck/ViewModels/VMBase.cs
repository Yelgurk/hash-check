using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace HashCheck.ViewModels
{
    public partial class VMBase : ObservableObject
    {
        public required ContentControl View { get; init; }

        public required IWindowContentService WindowContentService { get; init; }

        public void DisplayMainPage() => WindowContentService.Set<FileAwait>();

        public void DragOverAccess(object sender, DragEventArgs e)
        {
            e.DragEffects = e.DragEffects & DragDropEffects.Link;
            if (!e.Data.Contains(DataFormats.FileNames))
                e.DragEffects = DragDropEffects.None;
        }

        public async Task DropObjectForHash(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
                App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(e.Data.GetFileNames()!.ToArray());
        }

        public async Task DropObjectForComparing(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
                App.Host!.Services.GetRequiredService<HashComputator>().PathTreeParser(e.Data.GetFileNames()!.ToArray(), true);
        }
    }

    public static class ContentControlExtensions
    {
        public static Window? ParentWindow(this Control control)
        {
            StyledElement? parentControl = control;

            while(parentControl is not Window and not null)
                parentControl = parentControl.Parent;

            return parentControl as Window;
        }
    }
}
