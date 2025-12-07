using System;

namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Marks a property as included in the markdown table output
/// </summary>
/// <remarks>This is more for use on complex types that would ordinarily be ignored by the default options</remarks>
[AttributeUsage(AttributeTargets.Property)]
public class IncludeInOutputAttribute : Attribute
{
}