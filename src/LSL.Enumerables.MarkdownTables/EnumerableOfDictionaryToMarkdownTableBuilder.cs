using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

internal class EnumerableOfDictionaryToMarkdownTableBuilder(
    EnumerableOfDictionaryToMarkdownTableBuilderOptions options,
    ValueExtractor valueExtractor) : IEnumerableOfDictionaryToMarkdownTableBuilder
{
    public string CreateTable(IEnumerable<IDictionary<string, object>> dictionary)
    {
        throw new System.NotImplementedException();
    }
}
