using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable of dictionary to markdown table builder interface
/// </summary>
public interface IEnumerableOfDictionaryToMarkdownTableBuilder
{
    /// <summary>
    /// Creates a markdown table from a dictionary
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    string CreateTable(IEnumerable<IDictionary<string, object>> dictionary);
}
