namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Base class for markdown table builder options
/// </summary>
public abstract class BaseEnumerableToMarkdownTableBuilderOptions : BaseEnumerableToMarkdownTableBuilderOptionsNoMetaData
{
    internal IPropertyMetaDataProvider PropertyMetaDataProvider { get; set; } = new DefaultPropertyMetaDataProvider(new DefaultPropertyMetaDataProviderOptions());
}
