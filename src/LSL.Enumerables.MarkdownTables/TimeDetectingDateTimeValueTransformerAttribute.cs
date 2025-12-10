using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Time detecting date time value transformer attribute
/// </summary>
/// <param name="dateTimeFormat"></param>
/// <param name="dateOnlyFormat"></param>
[AttributeUsage(AttributeTargets.Property)]
public class TimeDetectingDateTimeValueTransformerAttribute(string dateTimeFormat, string dateOnlyFormat) : Attribute
{
    /// <summary>
    /// The date time format
    /// </summary>
    public string DateTimeFormat => dateTimeFormat;

    /// <summary>
    /// The date only format
    /// </summary>
    public string DateOnlyFormat => dateOnlyFormat;    
}