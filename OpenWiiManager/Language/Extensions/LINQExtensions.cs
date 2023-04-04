using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class LINQExtensions
    {
        public static IEnumerable<TItem> WhereNotNull<TItem>(this IEnumerable<TItem> items)
        {
            return items.Where(i => i != null);
        }
        public static IEnumerable<TItem> WhereNotNull<TItem, TProperty>(this IEnumerable<TItem> items, Func<TItem, TProperty> predicate)
        {
            return items.Where(i => predicate(i) != null);
        }

        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction, int maxDegreeOfParallelism = 10)
        {
            var semaphoreSlim = new SemaphoreSlim(maxDegreeOfParallelism);
            var tcs = new TaskCompletionSource<object>();
            var exceptions = new ConcurrentBag<Exception>();
            bool addingCompleted = false;

            foreach (T item in source)
            {
                await semaphoreSlim.WaitAsync();
                _ = asyncAction(item).ContinueWith(t =>
                {
                    semaphoreSlim.Release();

                    if (t.Exception != null)
                    {
                        exceptions.Add(t.Exception);
                    }

                    if (Volatile.Read(ref addingCompleted) && semaphoreSlim.CurrentCount == maxDegreeOfParallelism)
                    {
                        tcs.TrySetResult(null);
                    }
                });
            }

            Volatile.Write(ref addingCompleted, true);
            await tcs.Task;
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
