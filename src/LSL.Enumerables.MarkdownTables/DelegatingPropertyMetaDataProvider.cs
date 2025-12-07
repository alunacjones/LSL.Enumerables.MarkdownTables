using System;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Delegating property meta data provider
/// </summary>
/// <param name="delegate"></param>
public class DelegatingPropertyMetaDataProvider(Func<PropertyInfo, PropertyMetaData> @delegate) : IPropertyMetaDataProvider
{
    /// <inheritdoc/>
    public PropertyMetaData CreateMetaData(PropertyInfo propertyInfo) => @delegate(propertyInfo);
}