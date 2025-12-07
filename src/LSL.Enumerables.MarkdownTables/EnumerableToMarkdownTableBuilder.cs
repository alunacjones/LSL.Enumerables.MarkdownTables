using System.Collections.Generic;
using System.Linq;
using System.Text;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableToMarkdownTableBuilder(
    EnumerableToMarkdownTableBuilderOptions options,
    PropertyMetaDataProviderRetriever propertyMetaDataProviderRetriever,
    ValueExtractor valueExtractor) : IEnumerableToMarkdownTableBuilder
{
    /// <inheritdoc/>
    public string CreateTable<T>(IEnumerable<T> items)
    {
        if (items.AssertNotNull(nameof(items)).Any() is false) return options.DefaultResultIfNoItems;
        
        var propertyMetaDataDictionary = propertyMetaDataProviderRetriever.GetPropertyMetaData<T>(options);
        var extractedValues = valueExtractor.ExtractValues(items, propertyMetaDataDictionary, options);        
        var headers = propertyMetaDataDictionary.GenerateHeaders();            
        var maxLengths = extractedValues.GetMaxLengths(headers);
        var justifiedHeaders = propertyMetaDataDictionary.GenerateJustifiedHeaders(headers, maxLengths);
        var separators = propertyMetaDataDictionary.GenerateHeaderSeparators(maxLengths);

        return new StringBuilder()
            .AppendLine($"| {string.Join(" | ", justifiedHeaders)} |")
            .AppendLine($"| {string.Join(" | ", separators)} |")
            .AddRecords(extractedValues, maxLengths, propertyMetaDataDictionary)
            .ToString();
    }
}