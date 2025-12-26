using System;
using System.Collections.Generic;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// MarkdownTableGeneratorEnumerableExtensions
/// </summary>
public static class MarkdownTableGeneratorEnumerableExtensions
{
    private static readonly IEnumerableToMarkdownTableBuilderFactory _factory = new EnumerableToMarkdownTableBuilderFactory();

    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> to a markdown table using the given options
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string ToMarkdownTable<T>(this IEnumerable<T> values, EnumerableToMarkdownTableBuilderOptions options = null) => 
        _factory.Build(options ?? DefaultOptions()).CreateTable(values);

    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> to a markdown table, configuring the options with the given delegate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static string ToMarkdownTable<T>(this IEnumerable<T> values, Action<EnumerableToMarkdownTableBuilderOptions> configurator)
    {
        var options = new EnumerableToMarkdownTableBuilderOptions();
        configurator.AssertNotNull(nameof(configurator))(options);

        return _factory.Build(options).CreateTable(values);
    }

    internal static EnumerableToMarkdownTableBuilderOptions DefaultOptions() => 
        new EnumerableToMarkdownTableBuilderOptions().AddDefaultValueTransformers();

    internal static EnumerableToMarkdownTableBuilderOptions<T> DefaultStronglyTypesOptions<T>() =>
        new EnumerableToMarkdownTableBuilderOptions<T>().AddDefaultValueTransformers();

    internal static EnumerableOfDictionaryToMarkdownTableBuilderOptions DefaultDictionaryOptions() =>
        new EnumerableOfDictionaryToMarkdownTableBuilderOptions().AddDefaultValueTransformers();
}

/// <summary>
/// Handler delegate for middleware pipelines
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <param name="context"></param>
/// <param name="next"></param>
/// <returns></returns>
public delegate TResult HandlerDelegate<in TContext, TResult>(TContext context, Func<TResult> next);
