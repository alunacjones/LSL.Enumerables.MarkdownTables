namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder factory
/// </summary>
public class EnumerableToMarkdownTableBuilderFactory : IEnumerableToMarkdownTableBuilderFactory
{
    /// <inheritdoc/>
    public IEnumerableToMarkdownTableBuilder Build(EnumerableToMarkdownTableBuilderOptions options = null) => 
        new EnumerableToMarkdownTableBuilder(
            options ?? MarkdownTableGeneratorEnumerableExtensions._defaultOptions,
            PropertyMetaDataProviderRetriever.Instance,
            ValueExtractor.Instance
        );
}
