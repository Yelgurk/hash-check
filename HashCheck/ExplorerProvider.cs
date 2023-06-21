using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HashCheck;

public static class Extensions
{
    public static async Task AddRangeAsync<T>(this ObservableCollection<T> observableCollection, 
        IAsyncEnumerable<T> range, CancellationToken ct = default)
    {
        await foreach (var item in range.WithCancellation(ct))
        {
            observableCollection.Add(item);
        }
    }

    public static T? Cast<T>(this object obj) where T : class => obj as T;

    public static T Do<T>(this T obj, Action<T> action)
    {
        action(obj);
        return obj;
    }
}

public class ExplorerProvider
{
    [DllImport("shell32.dll", SetLastError = true)]
    public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

    [DllImport("shell32.dll", SetLastError = true)]
    public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

    public static void OpenFolderAndSelectItem(string folderPath, string file)
    {
        IntPtr nativeFolder;
        uint psfgaoOut;
        SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

        if (nativeFolder == IntPtr.Zero)
            return;

        IntPtr nativeFile;
        SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

        IntPtr[] fileArray;
        if (nativeFile == IntPtr.Zero)
            fileArray = new IntPtr[0];
        else
            fileArray = new IntPtr[] { nativeFile };

        SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

        Marshal.FreeCoTaskMem(nativeFolder);
        if (nativeFile != IntPtr.Zero)
            Marshal.FreeCoTaskMem(nativeFile);
    }
}