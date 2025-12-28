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
                p.PropertyName,
                (source[p.PropertyName].HeaderProvider ?? DefaultHeaderProvider.Instance)
                .GetHeader(p)));

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

    public static IEnumerable<string> GenerateJustifiedHeaders(
        this IDictionary<string, PropertyMetaData> source,
        IEnumerable<KeyValuePair<string, string>> headers,
        IDictionary<string, int> maxLengths) => 
        source
            .Select(kvp => kvp.Value)
            .Where(m => m.IncludeInOutput)
            .Select((item, index) => headers.ElementAt(index).Value.PadWithJustification(item.Justification, maxLengths.ElementAt(index).Value));

    public static IEnumerable<string> GenerateHeaderSeparators(
        this IDictionary<string, PropertyMetaData> source,
        IDictionary<string, int> maxLengths) => 
        source
            .Select(kvp => kvp.Value)
            .Select((m, index) => m.Justification.CreateHeaderLine(maxLengths.ElementAt(index).Value));
}