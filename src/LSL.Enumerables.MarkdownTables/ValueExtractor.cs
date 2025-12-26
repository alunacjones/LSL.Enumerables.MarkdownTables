using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal class ValueExtractor
{
    internal static ValueExtractor Instance { get; } = new ValueExtractor();

    internal IEnumerable<IDictionary<string, string>> ExtractValues<T>(
        IEnumerable<T> values,
        IEnumerable<KeyValuePair<string, PropertyMetaData>> properties,
        BaseEnumerableToMarkdownTableBuilderOptions options)
    {
        var composite = CompositeHandlerFactory.CreateCompositeHandler(
            options.ValueTransformers.Select(v => new HandlerDelegate<object, string>(v.Transform))).Handler;

        return values.Select(v =>
        {
            return properties.Select(kvp => kvp.Value).Aggregate(new Dictionary<string, string>(), (agg, i) =>
            {
                var value = ((i.ValueTransformer is null 
                    ? composite(i.PropertyInfo.GetValue(v))
                    : i.ValueTransformer.Transform(
                        i.PropertyInfo.GetValue(v),
                        [ExcludeFromCodeCoverage] () => $"{i.PropertyInfo.GetValue(v)}")) 
                        ?? $"{i.PropertyInfo.GetValue(v)}")
                    .InternalReplaceLineEndings();

                agg.Add(i.PropertyInfo.Name, value);
                return agg;
            });
        });        
    }
}

internal class DictionaryValueExtractor
{
    
}