using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

internal static class SetExtensions
{
    public static ISet<T> AddRange<T>(this ISet<T> source, IEnumerable<T> values)
    {
        foreach (var value in  values)
        {
            source.Add(value);
        }

        return source;
    }
}
