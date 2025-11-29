namespace LSL.Enumerables.MarkdownTables;

public interface IEnumerableToMarkdownTableBuilderFactory
{
    IEnumerableToMarkdownTableBuilder Build(EnumerableToMarkdownTableBuilderOptions options);
}
