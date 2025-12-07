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
        source.AddValueTransformer(new DelegatingValueTransformer(handlerDelegate.AssertNotNull(nameof(handlerDelegate))));

    /// <summary>
    /// Use the given property meta data provider delegate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyMetaDataProvider"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions UsePropertyMetaDataProvider(
        this EnumerableToMarkdownTableBuilderOptions source,
        Func<PropertyInfo, PropertyMetaData> propertyMetaDataProvider) => 
        source.UsePropertyMetaDataProvider(new DelegatingPropertyMetaDataProvider(propertyMetaDataProvider));

    /// <summary>
    /// Use the given property meta data provider
    /// </summary>
    /// <param name="source"></param>
    /// <param name="propertyMetaDataProvider"></param>
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions UsePropertyMetaDataProvider(
        this EnumerableToMarkdownTableBuilderOptions source,
        IPropertyMetaDataProvider propertyMetaDataProvider)
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
    /// <returns></returns>
    public static EnumerableToMarkdownTableBuilderOptions AddDefaultValueTransformers(
        this EnumerableToMarkdownTableBuilderOptions source,
        string numberFormat = null,
        string integerFormat = null,
        string dateTimeFormat = null)
    {
        source.ValueTransformers.AddRange([
            new DateTimeValueTransformer(dateTimeFormat),  
            new NumberValueTransformer(numberFormat, integerFormat),
            new NullValueTransformer()
        ]);

        return source;
    }
}