using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder interface
/// </summary>
public interface IEnumerableToMarkdownTableBuilder
{
    /// <summary>
    /// Creates a markdown table from <paramref name="items"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    string CreateTable<T>(IEnumerable<T> items);
}
