namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable of dictionary to markdown table builder options
/// </summary>
public class EnumerableOfDictionaryToMarkdownTableBuilderOptions : BaseEnumerableToMarkdownTableBuilderOptionsNoMetaData
{
    internal HeaderDiscoveryMode HeaderDiscoveryMode = HeaderDiscoveryMode.FromFirstElement;
}