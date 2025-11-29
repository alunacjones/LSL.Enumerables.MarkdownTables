using System;

namespace LSL.Enumerables.MarkdownTables.Infrastructure;

/// <summary>
/// Guard extensions
/// </summary>
internal static class GuardExtensions
{
    public static T AssertNotNull<T>(
        this T value,
        string parameterName) => value ?? throw new ArgumentNullException(parameterName, "Argument cannot be null");
}