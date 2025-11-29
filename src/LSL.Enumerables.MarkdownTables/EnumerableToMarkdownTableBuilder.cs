using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableToMarkdownTableBuilder(EnumerableToMarkdownTableBuilderOptions options) : IEnumerableToMarkdownTableBuilder
{
    public string CreateTable<T>(IEnumerable<T> items)
    {
        items.AssertNotNull(nameof(items));
        var propertyMetaData = typeof(T).GetProperties().AsEnumerable()
            .Select(options.PropertyMetaDataProvider)
            .Where(p => p.IncludeInOutput);
        var propertyMetaDataDictionary = propertyMetaData.ToDictionary(m => m.PropertyInfo.Name, m => m);
        var properties = propertyMetaData
            .Where(m => m.IncludeInOutput);

        if (items.Any() is false) return options.DefaultResultIfNoItems;

        var extractedValues = ExtractValues(items, properties, options);

        var result = new StringBuilder();
        var headers = propertyMetaData
            .Where(p => p.IncludeInOutput)
            .Select(p => options.HeaderTransformer(p.PropertyInfo));
        var maxLengths = BuildMaxLengths(extractedValues, headers);

        var justifiedHeaders = propertyMetaData
            .Where(m => m.IncludeInOutput)
            .Select((item, index) => headers.ElementAt(index).PadWithJustification(item.Justification, maxLengths.ElementAt(index).Value));

        var separators = propertyMetaData.Select((m, index) => m.Justification.CreateHeaderLine(maxLengths.ElementAt(index).Value));
        result.AppendLine($"| {string.Join(" | ", justifiedHeaders)} |");
        result.AppendLine($"| {string.Join(" | ", separators)} |");

        AddRecords(result, extractedValues, maxLengths, propertyMetaDataDictionary);

        return result.ToString();
    }

    private static IDictionary<string, int> BuildMaxLengths(IEnumerable<IDictionary<string, string>> extractedValues, IEnumerable<string> headers)
    {
        return extractedValues.Concat([headers.ToDictionary(h => h, h => h)])
            .Aggregate(new Dictionary<string, int>(), (agg, i) =>
            {
                foreach (var value in i)
                {
                    agg[value.Key] = agg.TryGetValue(value.Key, out int propertyValue) ? Math.Max(propertyValue, value.Value.Length + 1) : value.Value.Length + 1;
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
                //agg.Add(i.Value.ReplaceLineEndings("<br/>").PadRight(maxLengths[i.Key]));
                agg.Add(i.Value.PadWithJustification(propertyMetaData[i.Key].Justification, maxLengths[i.Key]));
                return agg;
            });

            return agg.AppendLine($"| {string.Join(" | ", line)} |");
        });

    private static IEnumerable<IDictionary<string, string>> ExtractValues<T>(IEnumerable<T> values, IEnumerable<PropertyMetaData> properties, EnumerableToMarkdownTableBuilderOptions options)
    {
        var composite = CompositeHandlerFactory.CreateCompositeHandler(options.ValueTransformers.Select(v => new HandlerDelegate<object, string>(v.Transform))).Handler;

        return values.Select(v =>
        {
            return properties.Aggregate(new Dictionary<string, string>(), (agg, i) =>
            {
                agg.Add(i.PropertyInfo.Name, i.ValueTransformer is null ? composite(i.PropertyInfo.GetValue(v)) : i.ValueTransformer.Transform(i.PropertyInfo.GetValue(v), () => $"{i.PropertyInfo.GetValue(v)}"));
                return agg;
            });
        });
    }
}