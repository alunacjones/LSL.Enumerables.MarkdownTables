using System;
using System.Linq;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

internal static class MemberInfoExtensions
{
    public static bool TryGetAttribute<T>(this MemberInfo source, out T value)
        where T : Attribute
    {
        value = source.GetCustomAttribute<T>();
        return value is not null;
    }

    public static IValueTransformer GetFirstAttributeValueTransformer(this MemberInfo source, Action<AttributeContext> configurator)
    {
        var context = new AttributeContext(source);
        configurator(context);

        return context.Providers.Select(p => p()).FirstOrDefault(p => p is not null);
    }
}