using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A <see cref="DateTime"/> and <see cref="DateTimeOffset"/> value transformer
/// </summary>
/// <param name="dateTimeFormat"></param>
public class DateTimeValueTransformer(string dateTimeFormat = "d") : IValueTransformer
{
    private readonly string _dateTimeFormat = dateTimeFormat ?? "d";

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            DateTime dateTime => dateTime.ToString(_dateTimeFormat),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString(_dateTimeFormat),
            _ => next()
        };
    }
}
