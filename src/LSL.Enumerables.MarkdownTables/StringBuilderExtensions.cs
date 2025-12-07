using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSL.Enumerables.MarkdownTables;

internal static class StringBuilderExtensions
{
    public static StringBuilder AddRecords(
        this StringBuilder result,
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
}