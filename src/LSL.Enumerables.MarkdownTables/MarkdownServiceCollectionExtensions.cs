using System;
using LSL.Enumerables.MarkdownTables;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Markdown service collection extensions
/// </summary>
public static class MarkdownServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services needed to use the <see cref="IEnumerableToMarkdownTableBuilder{T}"/>
    /// to markdown table factory
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <param name="dictionaryBuilderConfigurator"></param>
    /// <returns></returns>
    public static IServicesEnumerableToMarkdownTableBuilder AddEnumerablesToMarkdown(
        this IServiceCollection source,
        Action<EnumerableToMarkdownTableBuilderOptions> configurator = null,
        Action<EnumerableOfDictionaryToMarkdownTableBuilderOptions> dictionaryBuilderConfigurator = null)
    {
        source.TryAddSingleton<IEnumerableToMarkdownTableBuilderFactory, EnumerableToMarkdownTableBuilderFactory>();

        source.TryAddSingleton(sp => sp
            .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
            .Build(configurator));

        source.TryAddSingleton(sp => sp
            .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
            .BuildForDictionary(dictionaryBuilderConfigurator));
            
        return new ServicesEnumerableToMarkdownTableBuilder(source);
    }
}
