using System.Numerics;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

public static class PropertyInfoExtensions
{
    public static Justification GetJustification(this PropertyInfo source)
    {
        return source.PropertyType == typeof(int)
            || source.PropertyType == typeof(decimal)
            || source.PropertyType == typeof(int?)
            || source.PropertyType == typeof(decimal?)
            || source.PropertyType == typeof(double)
            || source.PropertyType == typeof(double?)
            || source.PropertyType == typeof(long)
            || source.PropertyType == typeof(long?)
            || source.PropertyType == typeof(BigInteger)
            || source.PropertyType == typeof(BigInteger?)
            || source.PropertyType == typeof(byte)
            || source.PropertyType == typeof(byte?)
            ? Justification.Right
            : Justification.Left;
    }

    public static bool IsSimpleType(this PropertyInfo source)
    {
        return source.PropertyType.IsPrimitive
            || source.PropertyType.IsValueType
            || source.PropertyType.IsEnum
            || source.PropertyType.Equals(typeof(string))
            || source.PropertyType.Equals(typeof(decimal));
    }    
}