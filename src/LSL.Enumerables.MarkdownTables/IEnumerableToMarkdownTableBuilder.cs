using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

public interface IEnumerableToMarkdownTableBuilder
{
    string CreateTable<T>(IEnumerable<T> items);
}
