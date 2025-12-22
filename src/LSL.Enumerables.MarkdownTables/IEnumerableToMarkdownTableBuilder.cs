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

/// <summary>
/// Strongly-typed Enumerable to markdown table builder interface
/// </summary>
public interface IEnumerableToMarkdownTableBuilder<T>
{
    /// <summary>
    /// Creates a strongly-typed markdown table from <paramref name="items"/>
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    string CreateTable(IEnumerable<T> items);
}