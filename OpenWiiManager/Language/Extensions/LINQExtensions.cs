using System;
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
    }
}
