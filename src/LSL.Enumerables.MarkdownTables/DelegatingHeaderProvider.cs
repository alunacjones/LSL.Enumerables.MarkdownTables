using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A delegating header provider
/// </summary>
public class DelegatingHeaderProvider(Func<PropertyMetaData, string> @delegate) : IHeaderProvider
{
    /// <inheritdoc/>
    public string GetHeader(PropertyMetaData propertyInfo) => @delegate(propertyInfo);
}