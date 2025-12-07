using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <inheritdoc/>
public class DefaultHeaderProvider : IHeaderProvider
{
    /// <summary>
    /// Static instance of the <see cref="DefaultHeaderProvider"/>
    /// </summary>
    public static IHeaderProvider Instance { get; } = new DefaultHeaderProvider();

    /// <inheritdoc/>
    public string GetHeader(PropertyInfo propertyInfo) => propertyInfo.Name;
}
