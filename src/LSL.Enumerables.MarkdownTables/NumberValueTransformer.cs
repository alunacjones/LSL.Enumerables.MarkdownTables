using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A number formatter
/// </summary>
/// <param name="numberFormat"></param>
public class NumberValueTransformer(string numberFormat = "00,00.00") : IValueTransformer
{
    private readonly string _numberFormat = numberFormat ?? "00,00.00";

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            double doubleValue => doubleValue.ToString(_numberFormat),
            decimal decimalValue => decimalValue.ToString(_numberFormat),
            _ => next()  
        };
    }
}
