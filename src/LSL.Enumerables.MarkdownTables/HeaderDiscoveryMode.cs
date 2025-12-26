namespace LSL.Enumerables.MarkdownTables;

/// <summary>
/// Header discovery mode
/// </summary>
public enum HeaderDiscoveryMode
{
    /// <summary>
    /// Look at the first element of the dictionary
    /// to infer all the header names
    /// </summary>
    FromFirstElement,

    /// <summary>
    /// Look at all the elements of the dictionary
    /// to collect all the distinct header names
    /// </summary>
    FromAllElements
}