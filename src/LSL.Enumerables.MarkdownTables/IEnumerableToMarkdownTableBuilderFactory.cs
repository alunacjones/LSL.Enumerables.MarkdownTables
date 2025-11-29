namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder factory interface
/// </summary>
public interface IEnumerableToMarkdownTableBuilderFactory
{
    /// <summary>
    /// Builds a markdown table builder instance configured with <paramref name="options"/>
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IEnumerableToMarkdownTableBuilder Build(EnumerableToMarkdownTableBuilderOptions options = null);
}
