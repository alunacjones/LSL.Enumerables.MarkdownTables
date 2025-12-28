using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

internal class DefaultPropertyMetaDataProvider(BaseDefaultPropertyMetaDataProviderOptions options) : IPropertyMetaDataProvider
{
    public PropertyMetaData CreateMetaData(PropertyInfo propertyInfo) => 
        new PropertyMetaData(
            propertyInfo.Name,
            propertyInfo.GetValue,
            options.ResolveInclusionOfProperty(propertyInfo),
            propertyInfo.GetJustification(),
            propertyInfo.ResolveValueTransformerFromAttributes(),
            options.HeaderProvider);
}