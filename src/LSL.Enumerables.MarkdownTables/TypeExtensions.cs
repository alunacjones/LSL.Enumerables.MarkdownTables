using System;
using System.Collections.Generic;
using System.Numerics;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Type extensions
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines the justification of a type
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Justification GetJustification(this Type source) => 
        source is not null && _numberTypesLookup.Value.Contains(source)
        ? Justification.Right
        : Justification.Left;

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
}