using System;
using System.Numerics;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A number formatter
/// </summary>
/// <param name="numberFormat"></param>
/// <param name="integerFormat"></param>
public class NumberValueTransformer(string numberFormat = null, string integerFormat = null) : IValueTransformer
{
    /// <summary>
    /// The number format for <see cref="decimal"/>, <see cref="double"/> and <see cref="float"/> values
    /// </summary>
    public string NumberFormat => numberFormat ?? NumericFormats.DefaultNumberFormat;

    /// <summary>
    /// The number format for all integer types
    /// </summary>
    public string IntegerFormat => integerFormat ?? NumericFormats.DefaultIntegerFormat;

    /// <inheritdoc/>
    public string Transform(object value, Func<string> next)
    {
        return value switch
        {
            double doubleValue => doubleValue.ToString(NumberFormat),
            decimal decimalValue => decimalValue.ToString(NumberFormat),
            float floatValue => floatValue.ToString(NumberFormat),
            int intValue => intValue.ToString(IntegerFormat),
            long longValue => longValue.ToString(IntegerFormat),
            byte byteValue => byteValue.ToString(IntegerFormat),
            short shortValue => shortValue.ToString(IntegerFormat),
            BigInteger bigInteger => bigInteger.ToString(IntegerFormat),

            _ => next()  
        };
    }
}
