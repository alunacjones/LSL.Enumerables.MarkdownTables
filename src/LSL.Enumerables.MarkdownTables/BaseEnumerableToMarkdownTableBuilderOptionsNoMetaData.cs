using System;
using System.Collections.Generic;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Base class for all markdown table builder options
/// </summary>
public abstract class BaseEnumerableToMarkdownTableBuilderOptionsNoMetaData
{
    internal Guid Id { get; } = Guid.NewGuid();
    internal List<IValueTransformer> ValueTransformers { get; } = [];

    /// <summary>
    /// The default to return if the enumerable that is being processed
    /// has no items
    /// </summary>
    /// <remarks>Defaults to <see langword="null"/></remarks>
    internal string DefaultResultIfNoItems { get; set; } = null;
}