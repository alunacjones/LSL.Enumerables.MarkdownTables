using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Date time value transformer attribute
/// </summary>
/// <param name="dateTimeFormat"></param>
[AttributeUsage(AttributeTargets.Property)]
public class DateTimeValueTransformerAttribute(string dateTimeFormat) : Attribute
{
    /// <summary>
    /// The date time format or the property
    /// </summary>
    public string DateTimeFormat { get; } = dateTimeFormat;
}