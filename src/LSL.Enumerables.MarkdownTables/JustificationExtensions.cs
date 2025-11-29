namespace LSL.Enumerables.MarkdownTables;

internal static class JustificationExtensions
{

    public static string CreateHeaderLine(this Justification source, int maxLength) =>
        source switch
        {
            Justification.Right => "---:".PadLeft(maxLength, '-'),
            _ => ":---".PadRight(maxLength, '-')
        };

    public static string PadWithJustification(this string source, Justification justification, int maxLength) => 
        justification switch
        {
            Justification.Right => source.PadLeft(maxLength),
            _ => source.PadRight(maxLength)
        };
}