using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableOfDictionaryToMarkdownTableBuilder(
    EnumerableOfDictionaryToMarkdownTableBuilderOptions options,
    ValueExtractor valueExtractor) :
    BaseEnumerableToMarkdownTableBuilder(
        options,
        PropertyMetaDataProviderRetriever.Instance,
        DictionaryMetaDataProviderRetriever.Instance,
        valueExtractor), 
    IEnumerableOfDictionaryToMarkdownTableBuilder
{
    public string CreateTable(IEnumerable<IDictionary<string, object>> items) => InternalCreateTable(items);
}
