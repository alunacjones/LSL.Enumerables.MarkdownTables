namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable of dictionary to markdown table builder options extensions
/// </summary>
public static class EnumerableOfDictionaryToMarkdownTableBuilderOptionsExtensions
{
    /// <summary>
    /// Sets up the header discovery mode.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="headerDiscoveryMode"></param>
    /// <returns></returns>
    public static EnumerableOfDictionaryToMarkdownTableBuilderOptions WithHeaderDiscoveryMode(
        this EnumerableOfDictionaryToMarkdownTableBuilderOptions source,
        HeaderDiscoveryMode headerDiscoveryMode)
    {
        source.HeaderDiscoveryMode = headerDiscoveryMode;
        return source;
    }
}