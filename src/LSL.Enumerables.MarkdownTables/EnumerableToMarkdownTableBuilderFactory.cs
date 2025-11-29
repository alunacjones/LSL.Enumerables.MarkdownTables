namespace LSL.Enumerables.MarkdownTables;

public class EnumerableToMarkdownTableBuilderFactory : IEnumerableToMarkdownTableBuilderFactory
{
    public IEnumerableToMarkdownTableBuilder Build(EnumerableToMarkdownTableBuilderOptions options) => new EnumerableToMarkdownTableBuilder(options);
}
