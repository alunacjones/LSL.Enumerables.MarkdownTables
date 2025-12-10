namespace LSL.Enumerables.MarkdownTables.Tests;

public class InclusionAndExclusion(int id, string name, string description)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public Other Other = new("test");
    
    public int GetANumber() => Id;

    public string AField = name;
}
