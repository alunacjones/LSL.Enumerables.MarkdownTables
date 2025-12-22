using System.Collections.Generic;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal static class SetExtensions
{
    public static ISet<T> AddRange<T>(this ISet<T> source, IEnumerable<T> values) => 
        values.Aggregate(source, (agg, i) =>
        {
            agg.Add(i);
            return agg;
        });
}
