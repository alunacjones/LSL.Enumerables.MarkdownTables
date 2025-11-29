using System;
using FluentAssertions;
using Humanizer;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class ToMarkdownTests
{
    [Test]
    public void Test()
    {
        var result = new Stuff[]
        {
            new(12, "Als", "Just some more"),
            new(13, "Other", "An anonymous entity") 
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions()
        {
            DefaultResultIfNoItems = "nope"             
        }.AddValueTransformer((o, next) => o?.ToString())
        .UsePropertyMetaDataProvider(pi => 
        { 
            var result = new PropertyMetaData(pi, pi.IsSimpleType(), pi.GetJustification());
            if (pi.Name == nameof(Stuff.TheDouble))
            {
                result.ValueTransformer = new NumberValueTransformer("£00,00.00");
            }
            return result;

        })
        .UseHeaderTransformer(h => h.Humanize()));

        result.Should().Be(
            """
            |  Age | Name   | Description          | The date    | The decimal |   The double |
            | ---: | :----- | :------------------- | :---------- | ----------: | -----------: |
            |   12 | Als    | Just some more       | 01/02/2010  |  123,456.23 |  £654,321.32 |
            |   13 | Other  | An anonymous entity  | 01/02/2010  |    1,234.00 |    £4,321.00 |

            """.ReplaceLineEndings()
        );
    }

    public class Stuff(int age, string name, string description)
    {
        public int Age { get; set; } = age;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public Other Other { get; set; } = new("other");
        public DateTime TheDate { get; set; } = new DateTime(2010, 2, 1);
        public decimal TheDecimal { get; set; } = age == 12 ? 123456.23m : 1234m;
        public double TheDouble { get; set; } = age == 12 ? 654321.32 : 4321;

        public Other Child { get; set; } = new("asd");
    }

    public class Other(string name)
    {
        public string Name { get; } = name;
    }
}