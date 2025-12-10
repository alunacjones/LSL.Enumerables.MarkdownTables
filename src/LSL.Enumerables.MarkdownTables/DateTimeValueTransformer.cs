using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A <see cref="DateTime"/> and <see cref="DateTimeOffset"/> value transformer
/// </summary>
/// <param name="dateTimeFormat"></param>
public class DateTimeValueTransformer(string dateTimeFormat = null) : IValueTransformer
{
    /// <summary>
    /// The date time format
    /// </summary>    
    public string DateTimeFormat => dateTimeFormat ?? DateTimeFormats.DefaultDateTimeFormat;

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            DateTime dateTime => dateTime.ToString(DateTimeFormat),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString(DateTimeFormat),
            _ => next()
        };
    }
}