using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A <see cref="DateTime"/> and <see cref="DateTimeOffset"/> value transformer that will
/// format any values that have no time using <paramref name="dateOnlyFormat"/>
/// </summary>
/// <param name="dateTimeFormat"></param>
/// <param name="dateOnlyFormat"></param>
public class TimeDetectingDateTimeValueTransformer(
    string dateTimeFormat = null,
    string dateOnlyFormat = null) : IValueTransformer
{
    /// <summary>
    /// The date time format
    /// </summary>
    public string DateTimeFormat => dateTimeFormat ?? DateTimeFormats.DefaultDateTimeFormat;

    /// <summary>
    /// The date only format
    /// </summary>
    public string DateOnlyFormat => dateOnlyFormat ?? DateTimeFormats.DefaultDateOnlyFormat;

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            DateTime dateTime => dateTime.ToStringWithEmptyTimeDetection(DateTimeFormat, DateOnlyFormat),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToStringWithEmptyTimeDetection(DateTimeFormat, DateOnlyFormat),
            _ => next()
        };
    }
}
