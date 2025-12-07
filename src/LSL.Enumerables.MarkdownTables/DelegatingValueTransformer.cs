using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Delegating value transformer
/// </summary>
/// <param name="handlerDelegate"></param>
public class DelegatingValueTransformer(HandlerDelegate<object, string> handlerDelegate) : IValueTransformer
{
    /// <inheritdoc/>
    public string Transform(object value, Func<string> next) => handlerDelegate(value, next);
}