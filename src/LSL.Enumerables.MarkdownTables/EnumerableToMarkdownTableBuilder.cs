using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableToMarkdownTableBuilder(EnumerableToMarkdownTableBuilderOptions options) : IEnumerableToMarkdownTableBuilder
{
    public string CreateTable<T>(IEnumerable<T> items)
    {
        if (items.AssertNotNull(nameof(items)).Any() is false) return options.DefaultResultIfNoItems;
        
        var propertyMetaData = typeof(T).GetProperties().AsEnumerable()
            .Select(options.PropertyMetaDataProvider.CreateMetaData)
            .Where(p => p.IncludeInOutput);

        var propertyMetaDataDictionary = propertyMetaData.ToDictionary(m => m.PropertyInfo.Name, m => m);        
        var extractedValues = ExtractValues(items, propertyMetaData, options);
        var result = new StringBuilder();
        var headers = propertyMetaData
            .Select(p => new KeyValuePair<string, string>(
                p.PropertyInfo.Name,
                (propertyMetaDataDictionary[p.PropertyInfo.Name].HeaderProvider ?? DefaultHeaderProvider.Instance)
                .GetHeader(p.PropertyInfo)));
            
        var maxLengths = BuildMaxLengths(extractedValues, headers);

        var justifiedHeaders = propertyMetaData
            .Where(m => m.IncludeInOutput)
            .Select((item, index) => headers.ElementAt(index).Value.PadWithJustification(item.Justification, maxLengths.ElementAt(index).Value));

        var separators = propertyMetaData.Select((m, index) => m.Justification.CreateHeaderLine(maxLengths.ElementAt(index).Value));
        result.AppendLine($"| {string.Join(" | ", justifiedHeaders)} |");
        result.AppendLine($"| {string.Join(" | ", separators)} |");

        AddRecords(result, extractedValues, maxLengths, propertyMetaDataDictionary);

        return result.ToString();
    }

    private static IDictionary<string, int> BuildMaxLengths(IEnumerable<IDictionary<string, string>> extractedValues, IEnumerable<KeyValuePair<string, string>> headers)
    {
        return extractedValues.Concat([headers.ToDictionary(h => h.Key, h => h.Value)])
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

    private static StringBuilder AddRecords(
        StringBuilder result,
        IEnumerable<IDictionary<string, string>> extractedValues,
        IDictionary<string, int> maxLengths,
        IDictionary<string, PropertyMetaData> propertyMetaData) => 
        extractedValues.Aggregate(result, (agg, record) =>
        {
            var line = record.Aggregate(new List<string>(), (agg, i) =>
            {
                agg.Add(i.Value.PadWithJustification(propertyMetaData[i.Key].Justification, maxLengths[i.Key]));
                return agg;
            });

            return agg.AppendLine($"| {string.Join(" | ", line)} |");
        });

    private static IEnumerable<IDictionary<string, string>> ExtractValues<T>(IEnumerable<T> values, IEnumerable<PropertyMetaData> properties, EnumerableToMarkdownTableBuilderOptions options)
    {
        var composite = CompositeHandlerFactory.CreateCompositeHandler(
            options.ValueTransformers.Select(v => new HandlerDelegate<object, string>(v.Transform))).Handler;

        return values.Select(v =>
        {
            return properties.Aggregate(new Dictionary<string, string>(), (agg, i) =>
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