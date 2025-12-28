using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Property meta data
/// </summary>
/// <param name="propertyName"></param>
/// <param name="propertyValueGetter"></param>
/// <param name="includeInOutput"></param>
/// <param name="justification"></param>
/// <param name="valueTransformer"></param>
/// <param name="headerProvider"></param>
public sealed class PropertyMetaData(
    string propertyName,
    Func<object, object> propertyValueGetter,
    bool includeInOutput = true,
    Justification justification = Justification.Left,
    IValueTransformer valueTransformer = null,
    IHeaderProvider headerProvider = null)
{
    /// <summary>
    /// Determines if the property should be included in the output
    /// </summary>
    public bool IncludeInOutput { get; } = includeInOutput;

    /// <summary>
    /// The justification of the item
    /// </summary>
    public Justification Justification { get; } =  justification;

    /// <summary>
    /// The name of the property
    /// </summary>
    public string PropertyName { get; } = propertyName;

    /// <summary>
    /// The property value getter
    /// </summary>
    public Func<object, object> PropertyValueGetter { get; } = propertyValueGetter;
    
    /// <summary>
    /// An optional <see cref="IValueTransformer"/> for the property
    /// </summary>
    public IValueTransformer ValueTransformer { get; set; } = valueTransformer;

    /// <summary>
    /// The header provider
    /// </summary>
    public IHeaderProvider HeaderProvider { get; set; } = headerProvider;
}