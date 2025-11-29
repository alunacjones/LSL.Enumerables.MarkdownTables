namespace LSL.Enumerables.MarkdownTables;

internal static class StringExtensions
{
    public static string InternalReplaceLineEndings(this string source, string replacement) => 
        source.Replace("\r", string.Empty).Replace("\n", replacement);
}