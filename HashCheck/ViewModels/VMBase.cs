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
    public class VMBase : ObservableObject
    {
        public required ContentControl View { get; init; }

        public required WindowContentService WindowContentService { get; init; }

        public void DisplayMainPage() => (WindowContentService as IWindowContentService).Set<FileAwait>();
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
