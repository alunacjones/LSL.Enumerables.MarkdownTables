using System;
using System.Collections.Generic;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

internal class AttributeContext(MemberInfo memberInfo)
{
    internal List<Func<IValueTransformer>> Providers = [];

    public AttributeContext For<T>(Func<T, IValueTransformer> provider)
        where T : Attribute
    {
        Providers.Add(() => memberInfo.TryGetAttribute<T>(out var attribute) 
            ? provider(attribute)
            : null
        );

        return this;
    }
}
