using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Default value transformer that just provides the string representation the value 
/// </summary>
public class DefaultValueTransformer : IValueTransformer
{
    /// <inheritdoc/>
    public string Transform(object value, Func<string> next) => $"{value}";
}
