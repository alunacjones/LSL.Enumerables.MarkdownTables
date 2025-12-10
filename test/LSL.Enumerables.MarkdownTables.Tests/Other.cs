namespace LSL.Enumerables.MarkdownTables.Tests;

public record Other(string Name)
{
#pragma warning disable CA1822 // Mark members as static
    internal int Id => 123;
#pragma warning restore CA1822 // Mark members as static
};
