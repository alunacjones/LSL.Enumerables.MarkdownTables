using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Default property meta data provider options extensions
/// </summary>
public static class DefaultPropertyMetaDataProviderOptionsExtensions
{
    /// <summary>
    /// Adds included properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static T IncludeProperties<T>(this T source, params string[] names) 
        where T : BaseDefaultPropertyMetaDataProviderOptions => 
        source.IncludeProperties(names.AssertNotNull(nameof(names)).AsEnumerable());

    /// <summary>
    /// Adds included properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static T IncludeProperties<T>(this T source, IEnumerable<string> names)
        where T : BaseDefaultPropertyMetaDataProviderOptions
    {
        source.AssertNotNull(nameof(source)).IncludedProperties.AddRange(names.AssertNotNull(nameof(names)));
        return source;
    }

    /// <summary>
    /// Adds excluded properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static T ExcludeProperties<T>(this T source, params string[] names) 
        where T : BaseDefaultPropertyMetaDataProviderOptions => 
        source.ExcludeProperties(names.AssertNotNull(nameof(names)).AsEnumerable());

    /// <summary>
    /// Adds excluded properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static T ExcludeProperties<T>(this T source, IEnumerable<string> names)
        where T : BaseDefaultPropertyMetaDataProviderOptions
    {
        source.AssertNotNull(nameof(source)).ExcludedProperties.AddRange(names.AssertNotNull(nameof(names)));
        return source;
    }

    /// <summary>
    /// Allows for configuration of included and excluded properties using lambda expressions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions ForClass<T>(
        this DefaultPropertyMetaDataProviderOptions source,
        Action<IStronglyTypedDefaultPropertyMetaDataProviderOptions<T>> configurator)
    {
        var options = new StronglyTypedDefaultPropertyMetaDataProviderOptions<T>(source.AssertNotNull(nameof(source)));
        configurator.AssertNotNull(nameof(configurator))(options);

        return source;
    }
    
    /// <summary>
    /// Include the provided properties in the output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions<T> IncludeProperties<T>(this DefaultPropertyMetaDataProviderOptions<T> source, IEnumerable<Expression<Func<T, object>>> propertyExpressions)
    {
        var options = new StronglyTypedDefaultPropertyMetaDataProviderOptions<T>(source.AssertNotNull(nameof(source)));
        options.IncludeProperties(propertyExpressions);

        return source;
    }

    /// <summary>
    /// Include the provided properties in the output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions<T> IncludeProperties<T>(this DefaultPropertyMetaDataProviderOptions<T> source, params Expression<Func<T, object>>[] propertyExpressions) => 
        source.IncludeProperties(propertyExpressions.AsEnumerable());

    /// <summary>
    /// Exclude the provided properties from the output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions<T> ExcludeProperties<T>(this DefaultPropertyMetaDataProviderOptions<T> source, IEnumerable<Expression<Func<T, object>>> propertyExpressions)
    {
        var options = new StronglyTypedDefaultPropertyMetaDataProviderOptions<T>(source.AssertNotNull(nameof(source)));
        options.ExcludeProperties(propertyExpressions);

        return source;        
    }

    /// <summary>
    /// Exclude the provided properties from the output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="propertyExpressions"></param>
    /// <returns></returns>

    public static DefaultPropertyMetaDataProviderOptions<T> ExcludeProperties<T>(this DefaultPropertyMetaDataProviderOptions<T> source, params Expression<Func<T, object>>[] propertyExpressions) => 
        source.ExcludeProperties(propertyExpressions.AsEnumerable());

    /// <summary>
    /// Use the provided <see cref="IHeaderProvider"/> for header names
    /// </summary>
    /// <param name="source"></param>
    /// <param name="headerProvider"></param>
    /// <returns></returns>
    public static T UseHeaderProvider<T>(
        this T source,
        IHeaderProvider headerProvider)
        where T : BaseDefaultPropertyMetaDataProviderOptions
    {
        source.AssertNotNull(nameof(source)).HeaderProvider = headerProvider.AssertNotNull(nameof(headerProvider));
        return source;
    }

    /// <summary>
    /// Use the provided delegate for header names
    /// </summary>
    /// <param name="source"></param>
    /// <param name="headerProvider"></param>
    /// <returns></returns>
    public static T UseHeaderProvider<T>(
        this T source,
        Func<PropertyInfo, string> headerProvider) 
        where T : BaseDefaultPropertyMetaDataProviderOptions => 
        source.UseHeaderProvider(new DelegatingHeaderProvider(headerProvider.AssertNotNull(nameof(headerProvider))));
}