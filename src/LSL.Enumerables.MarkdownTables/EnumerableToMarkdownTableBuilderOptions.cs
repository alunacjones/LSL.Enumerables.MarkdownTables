using System;
using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder options
/// </summary>
public class EnumerableToMarkdownTableBuilderOptions
{
    internal Guid Id { get; } = Guid.NewGuid();
    internal List<IValueTransformer> ValueTransformers { get; } = [];
    internal IPropertyMetaDataProvider PropertyMetaDataProvider { get; set; } = new DefaultPropertyMetaDataProvider(new());

    /// <summary>
    /// The default to return if the enumerable that is being processed
    /// has no items
    /// </summary>
    /// <remarks>Defaults to <see langword="null"/></remarks>s
    internal string DefaultResultIfNoItems { get; set; } = null;
}