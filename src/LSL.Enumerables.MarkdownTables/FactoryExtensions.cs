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
    public static IEnumerableToMarkdownTableBuilder Build(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableToMarkdownTableBuilderOptions> configurator = null) => 
        source.Build(configurator.ConfigureOptions(new EnumerableToMarkdownTableBuilderOptions()));

    /// <summary>
    /// Builds a strongly-typed markdown table builder instance configured with <paramref name="configurator"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IEnumerableToMarkdownTableBuilder<T> Build<T>(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableToMarkdownTableBuilderOptions<T>> configurator = null) => 
        source.Build(configurator.ConfigureOptions(new EnumerableToMarkdownTableBuilderOptions<T>()));

    /// <summary>
    /// Builds a dictionary-based markdown table builder instance configured with <paramref name="configurator"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IEnumerableOfDictionaryToMarkdownTableBuilder BuildForDictionary(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableOfDictionaryToMarkdownTableBuilderOptions> configurator = null) => 
        source.BuildForDictionary(configurator.ConfigureOptions(new EnumerableOfDictionaryToMarkdownTableBuilderOptions()));
}