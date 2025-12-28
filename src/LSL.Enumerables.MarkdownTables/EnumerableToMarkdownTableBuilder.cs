using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableToMarkdownTableBuilder(
    BaseEnumerableToMarkdownTableBuilderOptions options,
    PropertyMetaDataProviderRetriever propertyMetaDataProviderRetriever,
    DictionaryMetaDataProviderRetriever dictionaryMetaDataProviderRetriever,
    ValueExtractor valueExtractor) :
    BaseEnumerableToMarkdownTableBuilder(
        options,
        propertyMetaDataProviderRetriever,
        dictionaryMetaDataProviderRetriever,
        valueExtractor), 
    IEnumerableToMarkdownTableBuilder
{
    /// <inheritdoc/>
    public string CreateTable<T>(IEnumerable<T> items) => InternalCreateTable(items);
}

internal class EnumerableToMarkdownTableBuilder<T>(
    BaseEnumerableToMarkdownTableBuilderOptions options,
    PropertyMetaDataProviderRetriever propertyMetaDataProviderRetriever,
    DictionaryMetaDataProviderRetriever dictionaryMetaDataProviderRetriever,
    ValueExtractor valueExtractor) :
    BaseEnumerableToMarkdownTableBuilder(
        options, 
        propertyMetaDataProviderRetriever, 
        dictionaryMetaDataProviderRetriever, 
        valueExtractor),  
    IEnumerableToMarkdownTableBuilder<T>
{
    public string CreateTable(IEnumerable<T> items) => InternalCreateTable(items);
}