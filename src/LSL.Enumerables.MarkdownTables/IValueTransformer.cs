using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Value transformer
/// </summary>
public interface IValueTransformer
{
    /// <summary>
    /// Either transforms the value or calls next to perform the transformation
    /// </summary>
    /// <param name="value"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    string Transform(object value, Func<string> next);
}