using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Strongly typed default property meta data provider options interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IStronglyTypedDefaultPropertyMetaDataProviderOptions<T>
{
    /// <summary>
    /// Include the provided properties for output
    /// </summary>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> IncludeProperties(IEnumerable<Expression<Func<T, object>>> propertyExpressions);

    /// <summary>
    /// Exclude the provided properties from the output
    /// </summary>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> ExcludeProperties(IEnumerable<Expression<Func<T, object>>> propertyExpressions);
}