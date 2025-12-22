using System;
using LSL.Enumerables.MarkdownTables;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Enumerable to markdown table builder extensions
/// </summary>
public static class ServicesEnumerableToMarkdownTableBuilderExtensions
{
    /// <summary>
    /// Adds a strongly-typed enumerable to markdown table builder
    /// that accepts <typeparamref name="T"/> enumerables
    /// </summary>
    /// <remarks>
    /// A <see cref="IEnumerableToMarkdownTableBuilder{T}"/> should be injected into any consuming
    /// classes.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IServicesEnumerableToMarkdownTableBuilder AddBuilder<T>(
        this IServicesEnumerableToMarkdownTableBuilder source,
        Action<EnumerableToMarkdownTableBuilderOptions<T>> configurator = null)
    {        
        source.Services.TryAddSingleton(sp => sp
            .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
            .Build(configurator)
        );

        return source;
    }
}