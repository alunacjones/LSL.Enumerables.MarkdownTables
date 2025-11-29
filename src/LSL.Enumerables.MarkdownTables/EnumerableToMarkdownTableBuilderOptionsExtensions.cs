using System;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// EnumerableToMarkdownTableBuilderOptionsExtensions
/// </summary>
public static class EnumerableToMarkdownTableBuilderOptionsExtensions
{
    /// <summary>
    /// Use the given header transformer delegate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="headerTransformer"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions UseHeaderTransformer(this EnumerableToMarkdownTableBuilderOptions source, Func<PropertyInfo, string> headerTransformer)
    {
        source.AssertNotNull(nameof(source)).HeaderTransformer = headerTransformer.AssertNotNull(nameof(headerTransformer));
        return source;
    }

    /// <summary>
    /// Add a value transformer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="valueTransformer"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions AddValueTransformer(this EnumerableToMarkdownTableBuilderOptions source, IValueTransformer valueTransformer)
    {
        source.AssertNotNull(nameof(source)).ValueTransformers.Add(valueTransformer.AssertNotNull(nameof(valueTransformer)));
        return source;
    }
    
    /// <summary>
    /// Add a value transformer delegate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="handlerDelegate"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions AddValueTransformer(this EnumerableToMarkdownTableBuilderOptions source, HandlerDelegate<object, string> handlerDelegate) => 
        source.AddValueTransformer(new DelegatingValueTransformer(handlerDelegate));

    /// <summary>
    /// Use the given property meta data provider delegate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyMetaDataProvider"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions UsePropertyMetaDataProvider(
        this EnumerableToMarkdownTableBuilderOptions source,
        Func<PropertyInfo, PropertyMetaData> propertyMetaDataProvider)
    {
        source.PropertyMetaDataProvider = propertyMetaDataProvider.AssertNotNull(nameof(propertyMetaDataProvider));
        return source;
    }

    /// <summary>
    /// Add the default value transformers
    /// </summary>
    /// <remarks>
    /// <para>
    ///     The following value transformers are added:
    /// </para>
    /// <list type="table">
    ///   <listheader>
    ///       <term><see cref="DateTimeValueTransformer"/></term>
    ///       <description>Formats a <see cref="DateTime"/> or <see cref="DateTimeOffset"/> as a short date. The format string can be specified with <paramref name="dateTimeFormat"/></description>
    ///   </listheader>
    ///   <item>
    ///       <term><see cref="NumberValueTransformer" /></term>
    ///       <description>Formats numbers prettily i.e. with thousand separators (only used for decimal and double types). The format string can be specified with <paramref name="numberFormat"/></description>
    ///   </item>
    ///   <item>
    ///       <term><see cref="DefaultValueTransformer" /></term>
    ///       <description>The fallback transformer that just converts the property value to a string. Can be omitted using <paramref name="includeFallback"/></description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="includeFallback"></param>
    /// <param name="numberFormat"></param>
    /// <param name="dateTimeFormat"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions AddDefaultValueTransformers(
        this EnumerableToMarkdownTableBuilderOptions source,
        bool includeFallback = true,
        string numberFormat = null,
        string dateTimeFormat = null)
    {
        source.ValueTransformers.AddRange([
            new DateTimeValueTransformer(numberFormat),
            new NumberValueTransformer(dateTimeFormat)
        ]);

        if (includeFallback) source.ValueTransformers.Add(new DefaultValueTransformer());

        return source;
    }
}

/// <summary>
/// Delegating value transformer
/// </summary>
/// <param name="handlerDelegate"></param>
public class DelegatingValueTransformer(HandlerDelegate<object, string> handlerDelegate) : IValueTransformer
{
    /// <inheritdoc/>
    public string Transform(object value, Func<string> next) => handlerDelegate(value, next);
}