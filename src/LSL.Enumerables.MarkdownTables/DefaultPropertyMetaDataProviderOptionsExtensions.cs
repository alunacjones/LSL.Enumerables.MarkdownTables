using System;
using System.Collections.Generic;
using System.Linq;
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
        source.IncludeProperties(names.AsEnumerable());

    /// <summary>
    /// Adds included properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions IncludeProperties(this DefaultPropertyMetaDataProviderOptions source, IEnumerable<string> names)
    {
        source.IncludedProperties.AddRange(names);
        return source;
    }

    /// <summary>
    /// Adds excluded properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions ExcludeProperties(this DefaultPropertyMetaDataProviderOptions source, params string[] names) => 
        source.ExcludeProperties(names.AsEnumerable());

    /// <summary>
    /// Adds excluded properties
    /// </summary>
    /// <param name="source"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static DefaultPropertyMetaDataProviderOptions ExcludeProperties(this DefaultPropertyMetaDataProviderOptions source, IEnumerable<string> names)
    {
        source.ExcludedProperties.AddRange(names);
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
        var options = new StronglyTypedDefaultPropertyMetaDataProviderOptions<T>(source);
        configurator.AssertNotNull(nameof(configurator))(options);

        return source;
    }
}