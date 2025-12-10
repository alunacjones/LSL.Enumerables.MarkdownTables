using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Property name expression extensions
/// </summary>
public static class PropertyNameExpressionExtensions
{
    /// <summary>
    /// Gets the property info of a provided member expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> source)
    {
        if (source.Body is UnaryExpression unaryExpression && 
            unaryExpression.Operand is MemberExpression unaryMemberExpression &&
            unaryMemberExpression.Member is PropertyInfo unaryPropertyInfo && 
            unaryMemberExpression.IsATopLevelMemberExpression())
        {
            return unaryPropertyInfo;
        }

        if (source.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("propertyName must be a top-level property member expression", nameof(source));
        }

        if (memberExpression.Member is not PropertyInfo propertyInfo || !memberExpression.IsATopLevelMemberExpression())
        {
            throw new ArgumentException("propertyName must be a top-level property member expression", nameof(source));
        }

        return propertyInfo;
    }

    /// <summary>
    /// Gets the name of the property from the given expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetPropertyName<T>(this Expression<Func<T, object>> source) => source.GetPropertyInfo().Name;

    /// <summary>
    /// Tests a member expression accesses a top-level property of the target object
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsATopLevelMemberExpression(this MemberExpression source) => !source.Expression.ToString().Contains(".");
}