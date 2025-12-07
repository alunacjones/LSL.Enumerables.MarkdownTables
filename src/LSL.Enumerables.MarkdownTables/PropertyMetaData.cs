using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Property meta data
/// </summary>
/// <param name="propertyInfo"></param>
/// <param name="includeInOutput"></param>
/// <param name="justification"></param>
/// <param name="valueTransformer"></param>
/// <param name="headerProvider"></param>
public class PropertyMetaData(
    PropertyInfo propertyInfo,
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
    /// The <see cref="PropertyInfo"/>
    /// </summary>
    public PropertyInfo PropertyInfo { get; } = propertyInfo;

    /// <summary>
    /// An optional <see cref="IValueTransformer"/> for the property
    /// </summary>
    public IValueTransformer ValueTransformer { get; set; } = valueTransformer;

    /// <summary>
    /// The header provider
    /// </summary>
    public IHeaderProvider HeaderProvider { get; set; } = headerProvider;
}
