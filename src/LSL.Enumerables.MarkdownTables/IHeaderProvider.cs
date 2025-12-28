using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Header provider
/// </summary>
public interface IHeaderProvider
{
    /// <summary>
    /// Generates a header name from <paramref name="propertyInfo"/>
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public string GetHeader(PropertyMetaData propertyInfo);
}