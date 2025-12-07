using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Transforms a null value to <c>`null`</c>
/// </summary>
public class NullValueTransformer : IValueTransformer
{
    /// <inheritdoc/>
    public string Transform(object value, Func<string> next) => value is null ? "`null`" : next();
}