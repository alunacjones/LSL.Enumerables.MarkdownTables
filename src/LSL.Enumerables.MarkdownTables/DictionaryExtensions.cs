using System.Collections.Generic;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal static class DictionaryExtensions
{
    public static TValue InternalGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue) => 
        source.ContainsKey(key) ? source[key] : defaultValue;

    public static IEnumerable<KeyValuePair<string, string>> GenerateHeaders(this IDictionary<string, PropertyMetaData> source) => 
        source
            .Select(kvp => kvp.Value)
            .Select(p => new KeyValuePair<string, string>(
                p.PropertyInfo.Name,
                (source[p.PropertyInfo.Name].HeaderProvider ?? DefaultHeaderProvider.Instance)
                .GetHeader(p.PropertyInfo)));

    public static IDictionary<string, int> GetMaxLengths(
        this IEnumerable<IDictionary<string, string>> extractedValues,
        IEnumerable<KeyValuePair<string, string>> headers) =>
        extractedValues.Concat([headers.ToDictionary(h => h.Key, h => h.Value)])
            .Aggregate(new Dictionary<string, int>(), (agg, i) =>
            {
                foreach (var value in i)
                {
                    agg[value.Key] = new int[]
                    {
                        agg.InternalGetValueOrDefault(value.Key, 1), value.Value.Length + 1, 4
                    }.Max();
                }
                return agg;
            });
}