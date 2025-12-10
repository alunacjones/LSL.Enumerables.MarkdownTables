using System;
using System.Linq;
using System.Linq.Expressions;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Strongly typed default property meta data provider options extensions
/// </summary>
public static class StronglyTypedDefaultPropertyMetaDataProviderOptionsExtensions
{
    /// <summary>
    /// Include the provided properties for output
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>    
    public static IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> IncludeProperties<T>(
        this IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> source,
        params Expression<Func<T, object>>[] propertyExpressions) => 
        source.IncludeProperties(propertyExpressions.AsEnumerable());

    /// <summary>
    /// Exclude the provided properties from the output
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    public static IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> ExcludeProperties<T>(
        this IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> source,
        params Expression<Func<T, object>>[] propertyExpressions) => 
        source.ExcludeProperties(propertyExpressions.AsEnumerable());        
}