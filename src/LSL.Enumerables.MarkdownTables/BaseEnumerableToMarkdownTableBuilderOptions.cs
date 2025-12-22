using System;
using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Base class for markdown table builder options
/// </summary>
public abstract class BaseEnumerableToMarkdownTableBuilderOptions
{
    internal Guid Id { get; } = Guid.NewGuid();
    internal List<IValueTransformer> ValueTransformers { get; } = [];
    internal IPropertyMetaDataProvider PropertyMetaDataProvider { get; set; } = new DefaultPropertyMetaDataProvider(new DefaultPropertyMetaDataProviderOptions());

    /// <summary>
    /// The default to return if the enumerable that is being processed
    /// has no items
    /// </summary>
    /// <remarks>Defaults to <see langword="null"/></remarks>
    internal string DefaultResultIfNoItems { get; set; } = null;
    
}