using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        _factory.Build(options ?? _defaultOptions).CreateTable(values);

    private static readonly EnumerableToMarkdownTableBuilderOptions _defaultOptions = new EnumerableToMarkdownTableBuilderOptions()
    {
        
    };
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
