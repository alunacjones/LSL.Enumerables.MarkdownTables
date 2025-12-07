using System;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// A delegating header provider
/// </summary>
public class DelegatingHeaderProvider(Func<PropertyInfo, string> @delegate) : IHeaderProvider
{
    /// <inheritdoc/>
    public string GetHeader(PropertyInfo propertyInfo) => @delegate(propertyInfo);
}