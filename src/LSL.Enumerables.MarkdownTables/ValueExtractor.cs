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
        BaseEnumerableToMarkdownTableBuilderOptionsNoMetaData options)
    {
        var composite = CompositeHandlerFactory.CreateCompositeHandler(
            options.ValueTransformers.Select(v => new HandlerDelegate<object, string>(v.Transform))).Handler;

        return values.Select(v =>
        {
            return properties.Select(kvp => kvp.Value).Aggregate(new Dictionary<string, string>(), (agg, i) =>
            {
                var value = ((i.ValueTransformer is null 
                    ? composite(i.PropertyValueGetter(v))
                    : i.ValueTransformer.Transform(
                        i.PropertyValueGetter(v),
                        [ExcludeFromCodeCoverage] () => $"{i.PropertyValueGetter(v)}")) 
                        ?? $"{i.PropertyValueGetter(v)}")
                    .InternalReplaceLineEndings();

                agg.Add(i.PropertyName, value);
                return agg;
            });
        });        
    }
}

internal class DictionaryValueExtractor
{
    
}