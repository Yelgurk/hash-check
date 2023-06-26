using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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