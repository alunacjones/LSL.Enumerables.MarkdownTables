using System;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class Stuff(int age, string name, string description)
{
    public int Age { get; set; } = age;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    [IncludeInOutput]
    public Other Other { get; set; } = new("other");
    public DateTime TheDate { get; set; } = new DateTime(2010, 2, 1);

    [DateTimeValueTransformer("o")]
    public DateTimeOffset ADate { get; set; } = new DateTimeOffset(new DateTime(2020, 3, 2));

    [NumberValueTransformer("0", "C")]
    public decimal TheDecimal { get; set; } = age == 12 ? 123456.23m : 1234m;

    [NumberValueTransformer("C2", "C")]
    public double TheDouble { get; set; } = age == 12 ? 654321.32 : 4321;

    public Other Child { get; set; } = new("asd");
}