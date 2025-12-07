using System;
using System.Numerics;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A number formatter
/// </summary>
/// <param name="numberFormat"></param>
/// <param name="integerFormat"></param>
public class NumberValueTransformer(string numberFormat = "N2", string integerFormat = "N0") : IValueTransformer
{
    private readonly string _numberFormat = numberFormat ?? "N2";
    private readonly string _integerFormat = integerFormat ?? "N0";

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            double doubleValue => doubleValue.ToString(_numberFormat),
            decimal decimalValue => decimalValue.ToString(_numberFormat),
            float floatValue => floatValue.ToString(_numberFormat),
            int intValue => intValue.ToString(_integerFormat),
            long longValue => longValue.ToString(_integerFormat),
            byte byteValue => byteValue.ToString(_integerFormat),
            short shortValue => shortValue.ToString(_integerFormat),
            BigInteger bigInteger => bigInteger.ToString(_integerFormat),

            _ => next()  
        };
    }
}
