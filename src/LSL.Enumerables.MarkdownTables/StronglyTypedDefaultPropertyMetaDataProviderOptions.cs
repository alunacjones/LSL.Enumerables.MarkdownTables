using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LSL.Enumerables.MarkdownTables;

internal class StronglyTypedDefaultPropertyMetaDataProviderOptions<T>(DefaultPropertyMetaDataProviderOptions parentOptions) : IStronglyTypedDefaultPropertyMetaDataProviderOptions<T>
{
    public IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> ExcludeProperties(IEnumerable<Expression<Func<T, object>>> propertyExpressions)
    {
        parentOptions.ExcludeProperties(propertyExpressions.Select(p => p.GetPropertyName()));
        return this;
    }

    public IStronglyTypedDefaultPropertyMetaDataProviderOptions<T> IncludeProperties(IEnumerable<Expression<Func<T, object>>> propertyExpressions)
    {
        parentOptions.IncludeProperties(propertyExpressions.Select(p => p.GetPropertyName()));
        return this;
    }
}
