namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder factory extensions
/// </summary>
public static class EnumerableToMarkdownTableBuilderFactoryExtensions
{
    /// <summary>
    /// Creates a markdown table builder for dictionaries
    /// </summary>
    /// <param name="source"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IEnumerableOfDictionaryToMarkdownTableBuilder BuildForDictionary(
        this IEnumerableToMarkdownTableBuilderFactory source,
        EnumerableOfDictionaryToMarkdownTableBuilderOptions options = null) => 
        new EnumerableOfDictionaryToMarkdownTableBuilder(
            options ?? MarkdownTableGeneratorEnumerableExtensions.DefaultDictionaryOptions(),
            ValueExtractor.Instance
        );
}
