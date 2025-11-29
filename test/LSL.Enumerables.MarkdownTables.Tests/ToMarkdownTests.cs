using System;
using System.Collections.Generic;
using FluentAssertions;
using Humanizer;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class ToMarkdownTests
{
    [TestCase(false)]
    [TestCase(true)]
    public void Test(bool includeFallback)
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity") 
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions()
        {
            DefaultResultIfNoItems = "nope"             
        }
        .AddDefaultValueTransformers(includeFallback)
        .AddValueTransformer((o, next) => o?.ToString())
        .UsePropertyMetaDataProvider(pi => new(pi, pi.IsSimpleType(), pi.GetJustification(), pi.ResolveValueTransformerFromAttributes()))
        .UseHeaderTransformer(h => h.Name.Humanize()));

        result.Should().Be(
            """
            |  Age | Name   | Description                     | The date    | A date                             |  The decimal |   The double |
            | ---: | :----- | :------------------------------ | :---------- | :--------------------------------- | -----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |       123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |         1234 |    £4,321.00 |

            """.ReplaceLineEndings()
        );
    }

    [Test]
    public void GivenANull_ForTheTransformer_ItShouldThrowAnException()
    {
        new Action(() => new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity") 
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions().AddValueTransformer((HandlerDelegate<object, string>)null)))
        .Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    public void GivenACallToUseTheDefaults_ItShouldProduceTheExpectedResult()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity") 
        }.ToMarkdownTable();

        result.Should().Be(
            """
            |  Age | Name   | Description                     | TheDate     | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :----- | :------------------------------ | :---------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

            """.ReplaceLineEndings()
        );        
    }

    [Test]
    public void GivenSimpleInternalTypes_ItShouldProduceTheExpectedResult()
    {
        var result = new List<KeyValuePair<string, string>>
        {
            new("Key1", "Value1"),
            new("Key2", "Value2")
        }.ToMarkdownTable();

        result.Should().Be("""
            | Key   | Value   |
            | :---- | :------ |
            | Key1  | Value1  |
            | Key2  | Value2  |

            """.ReplaceLineEndings());
    }

    [Test]
    public void GivenACallToUseNoValueHandlers_ItShouldProduceTheExpectedResult()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity") 
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions());

        result.Should().Be(
            """
            |  Age | Name   | Description                     | TheDate              | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :----- | :------------------------------ | :------------------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

            """.ReplaceLineEndings()
        );        
    }    

    [Test]
    public void GivenACallWithNoItems_ItShouldReturnTheDefaultValue()
    {
        Array.Empty<Stuff>()
            .ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions { DefaultResultIfNoItems = "nowt" })
            .Should().Be("nowt");
    }
    
    public class Stuff(int age, string name, string description)
    {
        public int Age { get; set; } = age;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public Other Other { get; set; } = new("other");
        public DateTime TheDate { get; set; } = new DateTime(2010, 2, 1);

        [DateTimeValueTransformer("o")]
        public DateTimeOffset ADate { get; set; } = new DateTimeOffset(new DateTime(2020, 3, 2));

        [NumberValueTransformer("0")]
        public decimal TheDecimal { get; set; } = age == 12 ? 123456.23m : 1234m;

        [NumberValueTransformer("£00,00.00")]
        public double TheDouble { get; set; } = age == 12 ? 654321.32 : 4321;

        public Other Child { get; set; } = new("asd");
    }

    public class Other(string name)
    {
        public string Name { get; } = name;
    }
}