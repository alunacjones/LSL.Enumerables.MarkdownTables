using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Number value transformer attribute
/// </summary>
/// <param name="numberFormat"></param>
[AttributeUsage(AttributeTargets.Property)]
public class NumberValueTransformerAttribute(string numberFormat) : Attribute
{
    /// <summary>
    /// The number format for the property
    /// </summary>
    public string NumberFormat { get; } = numberFormat;
}
