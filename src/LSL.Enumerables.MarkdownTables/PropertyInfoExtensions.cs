using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Extensions for <see cref="PropertyInfo"/>
/// </summary>
public static class PropertyInfoExtensions
{
    private static readonly Lazy<ISet<Type>> _numberTypesLookup = new(
        () => new HashSet<Type>
        {
            typeof(int),
            typeof(decimal),
            typeof(int?),
            typeof(decimal?),
            typeof(double),
            typeof(double?),
            typeof(long),
            typeof(long?),
            typeof(BigInteger),
            typeof(BigInteger?),
            typeof(byte),
            typeof(byte?),
            typeof(short),
            typeof(short?),
            typeof(float),
            typeof(float?)            
        }
    );

    /// <summary>
    /// Determines the justification of a property based on its type
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Justification GetJustification(this PropertyInfo source) => 
        _numberTypesLookup.Value.Contains(source.PropertyType)
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
    public static IValueTransformer ResolveValueTransformerFromAttributes(this PropertyInfo source) => 
        source.GetFirstAttributeValueTransformer(c => c
            .For<NumberValueTransformerAttribute>(a => new NumberValueTransformer(a.NumberFormat, a.IntegerFormat))
            .For<DateTimeValueTransformerAttribute>(a => new DateTimeValueTransformer(a.DateTimeFormat))
        );

    /// <summary>
    /// Resolve output allowed from attributes and type
    /// </summary>
    /// <remarks>
    /// If the property is regarded a simple type or is decorated with the <see cref="IncludeInOutputAttribute"/>
    /// then it will return <see langword="true"/>, otherwise it returns <see langword="false"/>
    /// </remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool ResolveOutputAllowedFromAttributesAndType(this PropertyInfo source) => 
        source.IsSimpleType() || source.GetCustomAttribute<IncludeInOutputAttribute>() is not null;
}
