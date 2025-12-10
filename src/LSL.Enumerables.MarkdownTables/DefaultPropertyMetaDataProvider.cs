using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

internal class DefaultPropertyMetaDataProvider(DefaultPropertyMetaDataProviderOptions options) : IPropertyMetaDataProvider
{
    public PropertyMetaData CreateMetaData(PropertyInfo propertyInfo) => 
        new(
            propertyInfo,
            options.ResolveInclusionOfProperty(propertyInfo),
            propertyInfo.GetJustification(),
            propertyInfo.ResolveValueTransformerFromAttributes(),
            options.HeaderProvider);
}