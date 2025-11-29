using System;
using System.Numerics;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Extensions for <see cref="PropertyInfo"/>
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Determines the justification of a property based on its type
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Justification GetJustification(this PropertyInfo source) => 
        source.PropertyType == typeof(int) ||
            source.PropertyType == typeof(decimal) || 
            source.PropertyType == typeof(int?) || 
            source.PropertyType == typeof(decimal?) || 
            source.PropertyType == typeof(double) || 
            source.PropertyType == typeof(double?) || 
            source.PropertyType == typeof(long) || 
            source.PropertyType == typeof(long?) || 
            source.PropertyType == typeof(BigInteger) || 
            source.PropertyType == typeof(BigInteger?) || 
            source.PropertyType == typeof(byte) || 
            source.PropertyType == typeof(byte?)
            ? Justification.Right
            : Justification.Left;

    /// <summary>
    /// Determine if a property is a simple type
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsSimpleType(this PropertyInfo source) => source.AssertNotNull(nameof(source)).PropertyType.IsSimpleType();

    /// <summary>
    /// Determine if a type is a simple type
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsSimpleType(this Type source) =>
        source.AssertNotNull(nameof(source)).IsPrimitive ||
        source.IsValueType || 
        source.IsEnum || 
        source.Equals(typeof(string)) || 
        source.Equals(typeof(decimal));

    /// <summary>
    /// Resolves a value transformer based on attributes on the property
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IValueTransformer ResolveValueTransformerFromAttributes(this PropertyInfo source)
    {
        var numberAttribute = source.GetCustomAttribute<NumberValueTransformerAttribute>();
        if (numberAttribute is not null)
        {
            return new NumberValueTransformer(numberAttribute.NumberFormat);
        }

        var dateTimeAttribute = source.GetCustomAttribute<DateTimeValueTransformerAttribute>();
        if (dateTimeAttribute is not null)
        {
            return new DateTimeValueTransformer(dateTimeAttribute.DateTimeFormat);
        }

        return null;
    }
}