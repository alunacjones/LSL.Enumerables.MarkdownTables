using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Default property meta data provider options
/// </summary>
public sealed class DefaultPropertyMetaDataProviderOptions
{
    internal HashSet<string> IncludedProperties = [];
    internal HashSet<string> ExcludedProperties = [];

    internal bool ResolveInclusionOfProperty(PropertyInfo propertyInfo)
    {
        var hasIncluded = IncludedProperties.Any();
        var hasExcluded = ExcludedProperties.Any();

        if (!hasIncluded && !hasExcluded)
        {
            return propertyInfo.ResolveOutputAllowedFromAttributesAndType();
        }

        if (!hasIncluded && hasExcluded)
        {
            return !ExcludedProperties.Contains(propertyInfo.Name);            
        }

        return IncludedProperties.Contains(propertyInfo.Name) && !ExcludedProperties.Contains(propertyInfo.Name);
    }
}