using System;
using System.Reflection;
using LSL.Enumerables.MarkdownTables.Infrastructure;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// EnumerableToMarkdownTableBuilderOptionsExtensions
/// </summary>
public static class EnumerableToMarkdownTableBuilderOptionsExtensions
{
    public static EnumerableToMarkdownTableBuilderOptions UseHeaderTransformer(this EnumerableToMarkdownTableBuilderOptions source, Func<string, string> headerTransformer)
    {
        source.AssertNotNull(nameof(source)).HeaderTransformer = headerTransformer.AssertNotNull(nameof(headerTransformer));
        return source;
    }

    public static EnumerableToMarkdownTableBuilderOptions AddValueTransformer(this EnumerableToMarkdownTableBuilderOptions source, IValueTransformer valueTransformer)
    {
        source.AssertNotNull(nameof(source)).ValueTransformers.Add(valueTransformer.AssertNotNull(nameof(valueTransformer)));
        return source;
    }

    public static EnumerableToMarkdownTableBuilderOptions AddValueTransformer(this EnumerableToMarkdownTableBuilderOptions source, HandlerDelegate<object, string> handlerDelegate) => 
        source.AddValueTransformer(new DelegatingValueTransformer(handlerDelegate));

    public static EnumerableToMarkdownTableBuilderOptions UsePropertyMetaDataProvider(
        this EnumerableToMarkdownTableBuilderOptions source,
        Func<PropertyInfo, PropertyMetaData> propertyMetaDataProvider)
    {
        source.PropertyMetaDataProvider = propertyMetaDataProvider.AssertNotNull(nameof(propertyMetaDataProvider));
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