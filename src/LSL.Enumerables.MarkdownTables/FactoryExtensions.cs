using System;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Factory extensions
/// </summary>
public static class FactoryExtensions
{
    /// <summary>
    /// Builds a markdown table builder instance configured with <paramref name="configurator"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IEnumerableToMarkdownTableBuilder Build(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableToMarkdownTableBuilderOptions> configurator)
    {
        var options = new EnumerableToMarkdownTableBuilderOptions();
        configurator.AssertNotNull(nameof(configurator))(options);

        return source.Build(options);
    }
}
