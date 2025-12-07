using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Number value transformer attribute
/// </summary>
/// <param name="numberFormat"></param>
/// <param name="integerFormat"></param>
[AttributeUsage(AttributeTargets.Property)]
public class NumberValueTransformerAttribute(string numberFormat, string integerFormat) : Attribute
{
    /// <summary>
    /// The number format for the property if it is not an integer-type property.
    /// </summary>
    public string NumberFormat { get; } = numberFormat;

    /// <summary>
    /// The number format for integer-typed properties
    /// </summary>
    public string IntegerFormat { get; } = integerFormat;
}