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
    public static IEnumerableToMarkdownTableBuilder Build(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableToMarkdownTableBuilderOptions> configurator = null)
    {
        return source.Build(configurator is null ? null : ConfigureOptions(configurator));

        static EnumerableToMarkdownTableBuilderOptions ConfigureOptions(Action<EnumerableToMarkdownTableBuilderOptions> configurator)
        {
            var options = new EnumerableToMarkdownTableBuilderOptions();
            configurator.Invoke(options);

            return options;
        }
    }

    /// <summary>
    /// Builds a strongly-typed markdown table builder instance configured with <paramref name="configurator"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IEnumerableToMarkdownTableBuilder<T> Build<T>(this IEnumerableToMarkdownTableBuilderFactory source, Action<EnumerableToMarkdownTableBuilderOptions<T>> configurator = null)
    {
        return source.Build(configurator is null ? null : ConfigureOptions(configurator));

        static EnumerableToMarkdownTableBuilderOptions<T> ConfigureOptions(Action<EnumerableToMarkdownTableBuilderOptions<T>> configurator)
        {
            var options = new EnumerableToMarkdownTableBuilderOptions<T>();
            configurator.Invoke(options);

            return options;
        }
    }    
}
