using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Date time extensions
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Format's a <see cref="DateTime"/> with the <paramref name="dateOnlyFormat"/> if there is not time portion.
    /// Otherwise <paramref name="dateTimeFormat"/> is used
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dateTimeFormat"></param>
    /// <param name="dateOnlyFormat"></param>
    /// <returns></returns>
    public static string ToStringWithEmptyTimeDetection(this DateTime source, string dateTimeFormat, string dateOnlyFormat) => 
        source.IsDateOnly()
            ? source.ToString(dateOnlyFormat)
            : source.ToString(dateTimeFormat);

    /// <summary>
    /// Format's a <see cref="DateTimeOffset"/> with the <paramref name="dateOnlyFormat"/> if there is not time portion.
    /// Otherwise <paramref name="dateTimeFormat"/> is used
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dateTimeFormat"></param>
    /// <param name="dateOnlyFormat"></param>
    /// <returns></returns>
    public static string ToStringWithEmptyTimeDetection(this DateTimeOffset source, string dateTimeFormat, string dateOnlyFormat) => 
        source.IsDateOnly()
            ? source.ToString(dateOnlyFormat)
            : source.ToString(dateTimeFormat);

    /// <summary>
    /// Returns <see langword="true"/> if there is no time portion
    /// </summary>
    /// <remarks>
    /// <para>
    /// The implementation asserts that the following portions of the <see cref="DateTime"/> are all zero
    /// </para>
    /// <list type="bullet">
    ///     <item><see cref="DateTime.Hour"/></item>
    ///     <item><see cref="DateTime.Minute"/></item>
    ///     <item><see cref="DateTime.Second"/></item>
    ///     <item><see cref="DateTime.Millisecond"/></item>
    /// </list>
    /// </remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsDateOnly(this DateTime source) =>
        source.Hour == 0 && source.Minute == 0 && source.Second == 0 && source.Millisecond == 0;

    /// <summary>
    /// Returns <see langword="true"/> if there is no time portion
    /// </summary>
    /// <remarks>
    /// <para>
    /// The implementation asserts that the following portions of the <see cref="DateTimeOffset"/> are all zero
    /// </para>
    /// <list type="bullet">
    ///     <item><see cref="DateTimeOffset.Hour"/></item>
    ///     <item><see cref="DateTimeOffset.Minute"/></item>
    ///     <item><see cref="DateTimeOffset.Second"/></item>
    ///     <item><see cref="DateTimeOffset.Millisecond"/></item>
    /// </list>
    /// </remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsDateOnly(this DateTimeOffset source) =>
        source.Hour == 0 && source.Minute == 0 && source.Second == 0 && source.Millisecond == 0;
}