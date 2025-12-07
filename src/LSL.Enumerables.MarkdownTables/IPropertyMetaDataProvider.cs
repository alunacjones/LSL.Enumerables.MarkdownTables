using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Property meta data provider
/// </summary>
public interface IPropertyMetaDataProvider
{
    /// <summary>
    /// Creates a <see cref="PropertyMetaData"/> from <paramref name="propertyInfo"/>
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    PropertyMetaData CreateMetaData(PropertyInfo propertyInfo);
}