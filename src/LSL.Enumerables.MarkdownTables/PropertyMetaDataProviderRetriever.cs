using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal class PropertyMetaDataProviderRetriever
{
    internal static PropertyMetaDataProviderRetriever Instance { get; } = new PropertyMetaDataProviderRetriever();

    private static readonly IDictionary<string, IDictionary<string, PropertyMetaData>> _providers = 
        new ConcurrentDictionary<string, IDictionary<string, PropertyMetaData>>();

    internal IDictionary<string, PropertyMetaData> GetPropertyMetaData<T>(BaseEnumerableToMarkdownTableBuilderOptions options)
    {
        var type = typeof(T);
        var key = $"{options.Id}.{type.FullName}";

        if (_providers.ContainsKey(key) is false)
        {
            _providers[key] = type.GetProperties().AsEnumerable()
                .Select(options.PropertyMetaDataProvider.CreateMetaData)
                .Where(p => p.IncludeInOutput)
                .ToDictionary(m => m.PropertyName, m => m);
        }

        return _providers[key];
    }
}