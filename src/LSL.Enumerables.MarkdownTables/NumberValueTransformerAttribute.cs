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

/// <summary>
/// Marks a property as included in the markdown table output
/// </summary>
/// <remarks>This is more for use on complex types that would ordinarily be ignored by the default options</remarks>
[AttributeUsage(AttributeTargets.Property)]
public class IncludeInOutputAttribute : Attribute
{
}