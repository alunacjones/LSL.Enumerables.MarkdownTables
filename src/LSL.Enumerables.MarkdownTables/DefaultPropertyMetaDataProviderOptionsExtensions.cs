using System;
using System.Collections.Generic;
using System.Linq;
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
    public static DefaultPropertyMetaDataProviderOptions IncludeProperties(this DefaultPropertyMetaDataProviderOptions source, params string[] names) => 
        source.IncludeProperties(names.AssertNotNull(nameof(names)).AsEnumerable());

    /// <summary>
    /// Adds included properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions IncludeProperties(this DefaultPropertyMetaDataProviderOptions source, IEnumerable<string> names)
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
    public static DefaultPropertyMetaDataProviderOptions ExcludeProperties(this DefaultPropertyMetaDataProviderOptions source, params string[] names) => 
        source.ExcludeProperties(names.AssertNotNull(nameof(names)).AsEnumerable());

    /// <summary>
    /// Adds excluded properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions ExcludeProperties(this DefaultPropertyMetaDataProviderOptions source, IEnumerable<string> names)
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
    /// Use the provided <see cref="IHeaderProvider"/> for header names
    /// </summary>
    /// <param name="source"></param>
    /// <param name="headerProvider"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions UseHeaderProvider(
        this DefaultPropertyMetaDataProviderOptions source,
        IHeaderProvider headerProvider)
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
    public static DefaultPropertyMetaDataProviderOptions UseHeaderProvider(
        this DefaultPropertyMetaDataProviderOptions source,
        Func<PropertyInfo, string> headerProvider
    ) => 
    source.UseHeaderProvider(new DelegatingHeaderProvider(headerProvider.AssertNotNull(nameof(headerProvider))));
}