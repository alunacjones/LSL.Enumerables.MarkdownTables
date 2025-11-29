using System;
using System.Collections.Generic;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Enumerable to markdown table builder options
/// </summary>
public class EnumerableToMarkdownTableBuilderOptions
{
    internal Func<string, string> HeaderTransformer { get; set; } = header => header;
    internal List<IValueTransformer> ValueTransformers { get; } = [
        new DateTimeValueTransformer(),
        new NumberValueTransformer(),
        new DefaultValueTransformer()
    ];

    internal Func<PropertyInfo, PropertyMetaData> PropertyMetaDataProvider { get; set; } = p => new(p, p.IsSimpleType(), p.GetJustification());

    /// <summary>
    /// The default to return if the enumerable that is being processed
    /// has no items
    /// </summary>
    public string DefaultResultIfNoItems { get; set; } = null;
}