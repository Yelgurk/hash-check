using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using HashCheck.Views;

namespace HashCheck.ViewModels
{
    public partial class VMBase : ObservableObject
    {
        public required ContentControl View { get; init; }

        public required IWindowContentService WindowContentService { get; init; }

        public void DisplayMainPage() => WindowContentService.Set<FileAwait>();
    }

    public static class ContentControlExtensions
    {
        public static Window? ParentWindow(this IControl control)
        {
            IControl? parentControl = control;

            while(parentControl is not Window and not null)
                parentControl = parentControl.Parent;

            return parentControl as Window;
        }
    }
}
