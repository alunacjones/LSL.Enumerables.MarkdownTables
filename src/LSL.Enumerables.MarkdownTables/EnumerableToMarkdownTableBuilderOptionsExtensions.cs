using System;
using System.Collections.Generic;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// EnumerableToMarkdownTableBuilderOptionsExtensions
/// </summary>
public static class EnumerableToMarkdownTableBuilderOptionsExtensions
{
    /// <summary>
    /// Add a value transformer
    /// </summary>
    /// <param name="source"></param>
    /// <param name="valueTransformer"></param>
    /// <returns></returns>
    public static T AddValueTransformer<T>(this T source, IValueTransformer valueTransformer)
        where T : BaseEnumerableToMarkdownTableBuilderOptions
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
    public static T AddValueTransformer<T>(this T source, HandlerDelegate<object, string> handlerDelegate) 
        where T : BaseEnumerableToMarkdownTableBuilderOptions =>         
        source.AddValueTransformer(new DelegatingValueTransformer(handlerDelegate.AssertNotNull(nameof(handlerDelegate))));

    /// <summary>
    /// Use the given property meta data provider delegate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyMetaDataProvider"></param>
    /// <returns></returns>
    public static T UsePropertyMetaDataProvider<T>(
        this T source,
        Func<PropertyInfo, PropertyMetaData> propertyMetaDataProvider) 
        where T : BaseEnumerableToMarkdownTableBuilderOptions => 
        source.UsePropertyMetaDataProvider(new DelegatingPropertyMetaDataProvider(propertyMetaDataProvider));

    /// <summary>
    /// Use the given property meta data provider
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyMetaDataProvider"></param>
    /// <returns></returns>
    public static T UsePropertyMetaDataProvider<T>(
        this T source,
        IPropertyMetaDataProvider propertyMetaDataProvider)
        where T : BaseEnumerableToMarkdownTableBuilderOptions
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
    ///       <description>
    ///         Formats numbers prettily i.e. with thousand separators. 
    ///         The format strings for numeric and integer types can be specified 
    ///         with <paramref name="numberFormat"/> and <paramref name="integerFormat"/>
    ///       </description>
    ///   </item>
    ///   <item>
    ///       <term><see cref="NullValueTransformer"/></term>
    ///       <description>Formats a <see langword="null"/> value as <c>`null`</c></description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="numberFormat"></param>
    /// <param name="integerFormat"></param>
    /// <param name="dateTimeFormat"></param>
    /// <param name="useTimeDetectingDateTimeValueTransformer">Uses the <see cref="TimeDetectingDateTimeValueTransformer"/></param>
    /// <param name="dateOnlyFormat">This is only used if the <see cref="TimeDetectingDateTimeValueTransformer"/> is used</param>
    /// <returns></returns>
    public static T AddDefaultValueTransformers<T>(
        this T source,
        string numberFormat = null,
        string integerFormat = null,
        string dateTimeFormat = null,
        bool useTimeDetectingDateTimeValueTransformer = true,
        string dateOnlyFormat = null)
        where T : BaseEnumerableToMarkdownTableBuilderOptions
    {
        source.ValueTransformers.AddRange([
            useTimeDetectingDateTimeValueTransformer
                ? new TimeDetectingDateTimeValueTransformer(dateTimeFormat, dateOnlyFormat)
                : new DateTimeValueTransformer(dateTimeFormat),
            new NumberValueTransformer(numberFormat, integerFormat),
            new NullValueTransformer()
        ]);

        return source;
    }

    /// <summary>
    /// Use and configure the default property meta data provider
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions UseDefaultPropertyMetaDataProvider(
        this EnumerableToMarkdownTableBuilderOptions source,
        Action<DefaultPropertyMetaDataProviderOptions> configurator = null)
    {
        var options = new DefaultPropertyMetaDataProviderOptions();
        configurator?.Invoke(options);

        return source.UsePropertyMetaDataProvider(new DefaultPropertyMetaDataProvider(options));
    }

    /// <summary>
    /// Use and configure the default property meta data provider
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions<T> UseDefaultPropertyMetaDataProvider<T>(
        this EnumerableToMarkdownTableBuilderOptions<T> source,
        Action<DefaultPropertyMetaDataProviderOptions<T>> configurator = null)
    {
        var options = new DefaultPropertyMetaDataProviderOptions<T>();
        configurator?.Invoke(options);

        return source.UsePropertyMetaDataProvider(new DefaultPropertyMetaDataProvider(options));
    }

    /// <summary>
    /// Set the string value to return if the <see cref="IEnumerable{T}"/> is empty.
    /// </summary>
    /// <remarks>
    /// The default value is <see langword="null" />
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="emptyResult"></param>
    /// <returns></returns>
    public static T UseEmptyResult<T>(
        this T source,
        string emptyResult)
        where T : BaseEnumerableToMarkdownTableBuilderOptions
    {
        source.DefaultResultIfNoItems = emptyResult;
        return source;
    }
}