namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder factory
/// </summary>
public class EnumerableToMarkdownTableBuilderFactory : IEnumerableToMarkdownTableBuilderFactory
{
    /// <summary>
    /// A static instance for the factory
    /// </summary>
    public static IEnumerableToMarkdownTableBuilderFactory Instance = new EnumerableToMarkdownTableBuilderFactory();

    /// <inheritdoc/>
    public IEnumerableToMarkdownTableBuilder Build(EnumerableToMarkdownTableBuilderOptions options = null) => 
        new EnumerableToMarkdownTableBuilder(
            options ?? MarkdownTableGeneratorEnumerableExtensions.DefaultOptions(),
            PropertyMetaDataProviderRetriever.Instance,
            DictionaryMetaDataProviderRetriever.Instance,
            ValueExtractor.Instance
        );

    /// <inheritdoc/>
    public IEnumerableToMarkdownTableBuilder<T> Build<T>(EnumerableToMarkdownTableBuilderOptions<T> options = null) => 
        new EnumerableToMarkdownTableBuilder<T>(
            options ?? MarkdownTableGeneratorEnumerableExtensions.DefaultStronglyTypesOptions<T>(),
            PropertyMetaDataProviderRetriever.Instance,
            DictionaryMetaDataProviderRetriever.Instance,
            ValueExtractor.Instance
        );
}