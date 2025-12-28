using System.Collections.Generic;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal class DictionaryMetaDataProviderRetriever
{
    internal static DictionaryMetaDataProviderRetriever Instance = new();

    internal IDictionary<string, PropertyMetaData> GetPropertyMetaData(
        EnumerableOfDictionaryToMarkdownTableBuilderOptions options,
        IEnumerable<IDictionary<string, object>> data)
    {
        var first = data.First();
        return first.Aggregate(new Dictionary<string, PropertyMetaData>(), (agg, i) =>
        {
            var name = i.Key;
            agg.Add(
                name, 
                new PropertyMetaData(
                    name,
                    o => ((IDictionary<string, object>)o)[name],
                    true,
                    (first[name]?.GetType() ?? typeof(string)).GetJustification())
            );
            return agg; 
        });
    }
}