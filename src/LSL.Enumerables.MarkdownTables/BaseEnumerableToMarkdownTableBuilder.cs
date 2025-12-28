using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

internal abstract class BaseEnumerableToMarkdownTableBuilder(
    BaseEnumerableToMarkdownTableBuilderOptionsNoMetaData options,
    PropertyMetaDataProviderRetriever propertyMetaDataProviderRetriever,
    DictionaryMetaDataProviderRetriever dictionaryMetaDataProviderRetriever,
    ValueExtractor valueExtractor)
{   
    [ExcludeFromCodeCoverage] 
    protected string InternalCreateTable<T>(IEnumerable<T> items) => options switch
    {
        EnumerableOfDictionaryToMarkdownTableBuilderOptions dictionaryOptions => 
            BaseInternalCreateTable(items, () => dictionaryMetaDataProviderRetriever.GetPropertyMetaData(dictionaryOptions, (IEnumerable<IDictionary<string, object>>)items)),
        BaseEnumerableToMarkdownTableBuilderOptions defaultOptions => 
            BaseInternalCreateTable(items, () => propertyMetaDataProviderRetriever.GetPropertyMetaData<T>(defaultOptions)),
        _ => throw new NotImplementedException()
    };
        

    protected string BaseInternalCreateTable<T>(IEnumerable<T> items, Func<IDictionary<string, PropertyMetaData>> propertyMetaDataProviderDelegate)
    {
         if (items.AssertNotNull(nameof(items)).Any() is false) return options.DefaultResultIfNoItems;

        var propertyMetaDataDictionary = propertyMetaDataProviderDelegate();
        var headers = propertyMetaDataDictionary.GenerateHeaders();
        var extractedValues = valueExtractor.ExtractValues(items, propertyMetaDataDictionary, options);        
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