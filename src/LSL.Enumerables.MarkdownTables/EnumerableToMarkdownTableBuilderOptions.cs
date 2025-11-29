using System;
using System.Collections.Generic;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder options
/// </summary>
public class EnumerableToMarkdownTableBuilderOptions
{
    internal Func<PropertyInfo, string> HeaderTransformer { get; set; } = propertyInfo => propertyInfo.Name;
    internal List<IValueTransformer> ValueTransformers { get; } = [];

    internal Func<PropertyInfo, PropertyMetaData> PropertyMetaDataProvider { get; set; } = 
        p => new(p, p.IsSimpleType(), p.GetJustification(), p.ResolveValueTransformerFromAttributes());

    /// <summary>
    /// The default to return if the enumerable that is being processed
    /// has no items
    /// </summary>
    /// <remarks>Default to <see langword="null"/></remarks>s
    public string DefaultResultIfNoItems { get; set; } = null;
}