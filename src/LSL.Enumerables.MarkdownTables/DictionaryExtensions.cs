using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

internal static class DictionaryExtensions
{
    public static TValue InternalGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue) => 
        source.ContainsKey(key) ? source[key] : defaultValue;
}