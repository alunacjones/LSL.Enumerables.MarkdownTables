using System;
using System.Collections.Generic;
using System.Numerics;
using FluentAssertions;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;

namespace LSL.Enumerables.MarkdownTables.Tests;

public class ToMarkdownTests
{
    [Test]
    public void Test()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var humanizeHeaderProvider = new DelegatingHeaderProvider(p => p.Name.Humanize());

        var result = new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity"),
            new(14, null, "An anonymous entity")
        }.ToMarkdownTable(
            new EnumerableToMarkdownTableBuilderOptions()
            {
                DefaultResultIfNoItems = "nope"             
            }
            .AddDefaultValueTransformers()
            .AddValueTransformer((o, next) => o?.ToString())
            .UsePropertyMetaDataProvider(pi => new(
                pi, 
                pi.IsSimpleType(), 
                pi.GetJustification(), 
                pi.ResolveValueTransformerFromAttributes(),
                humanizeHeaderProvider))
        );

        result.Should().Be(
            """
            |  Age | Name    | Description                     | The date    | A date                             |  The decimal |   The double |
            | ---: | :------ | :------------------------------ | :---------- | :--------------------------------- | -----------: | -----------: |
            |   12 | Als     | Just some<br/>more things here  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |       123456 |  £654,321.32 |
            |   13 | Other   | An anonymous entity             | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |         1234 |    £4,321.00 |
            |   14 | `null`  | An anonymous entity             | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |         1234 |    £4,321.00 |

            """.ReplaceLineEndings()
        );
    }

    [Test]
    public void TestWithNoTimeDetectingTransformer()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new Stuff[]
        {
            new(12, "Als", "Just some\r\nmore things here"),
            new(13, "Other", "An anonymous entity"),
            new(14, null, "An anonymous entity")
        }.ToMarkdownTable(
            new EnumerableToMarkdownTableBuilderOptions()
            {
                DefaultResultIfNoItems = "nope"             
            }
            .AddDefaultValueTransformers(useTimeDetectingDateTimeValueTransformer: false)
        );

        result.Should().Be(
            """
            |  Age | Name    | Description                     | Other                   | TheDate              | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :------ | :------------------------------ | :---------------------- | :------------------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als     | Just some<br/>more things here  | Other { Name = other }  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other   | An anonymous entity             | Other { Name = other }  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |
            |   14 | `null`  | An anonymous entity             | Other { Name = other }  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

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
            |  Age | Name   | Description                     | Other                   | TheDate     | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :----- | :------------------------------ | :---------------------- | :---------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | Other { Name = other }  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | Other { Name = other }  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

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
    public void GivenSimpleInternalTypesAndGuidValue_ItShouldProduceTheExpectedResult()
    {
        var result = new List<KeyValuePair<string, Guid>>
        {
            new("Key1", Guid.Parse("8d3b6b24-e29f-45ac-a30f-aaf38785a25d")),
            new("Key2", Guid.Parse("b42d88ba-f19f-4c98-995d-9f80f99c5518"))
        }.ToMarkdownTable();

        result.Should().Be("""
            | Key   | Value                                 |
            | :---- | :------------------------------------ |
            | Key1  | 8d3b6b24-e29f-45ac-a30f-aaf38785a25d  |
            | Key2  | b42d88ba-f19f-4c98-995d-9f80f99c5518  |

            """.ReplaceLineEndings());
    }    

    [Test]
    public void GivenSimpleInternalTypesAndGuidValueAndCustomMetaData_ItShouldProduceTheExpectedResult()
    {
        var result = new List<KeyValuePair<string, Guid>>
        {
            new("Key1", Guid.Parse("8d3b6b24-e29f-45ac-a30f-aaf38785a25d")),
            new("Key2", Guid.Parse("b42d88ba-f19f-4c98-995d-9f80f99c5518"))
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions()
            .UsePropertyMetaDataProvider(p => new(
                p, 
                true, 
                Justification.Left,
                new DelegatingValueTransformer((v, _) => $"{v}"),
                headerProvider: new DelegatingHeaderProvider(pi => $"{pi.Name}!"))
        ));

        result.Should().Be("""
            | Key!  | Value!                                |
            | :---- | :------------------------------------ |
            | Key1  | 8d3b6b24-e29f-45ac-a30f-aaf38785a25d  |
            | Key2  | b42d88ba-f19f-4c98-995d-9f80f99c5518  |

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
            |  Age | Name   | Description                     | Other                   | TheDate              | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :----- | :------------------------------ | :---------------------- | :------------------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | Other { Name = other }  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | Other { Name = other }  | 01/02/2010 00:00:00  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

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
    
    [Test]
    public void WhenRegisteredInAServiceCollection_ItShouldResolveToTheExpectedConfiguration()
    {
        var provider = new ServiceCollection()
            .AddSingleton<IEnumerableToMarkdownTableBuilderFactory, EnumerableToMarkdownTableBuilderFactory>()
            .AddSingleton(sp => sp
                .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
                .Build(new EnumerableToMarkdownTableBuilderOptions()
                    .AddDefaultValueTransformers()
                    .UsePropertyMetaDataProvider(pi => new(pi, pi.ResolveOutputAllowedFromAttributesAndType(), pi.GetJustification()))
                ))
            .BuildServiceProvider();

        var result = provider.GetRequiredService<IEnumerableToMarkdownTableBuilder>()
            .CreateTable(new Stuff[]
            {
                new(12, "Als", "Just some\r\nmore things here"),
                new(13, "Other", "An anonymous entity") 
            });

        result.Should().Be(
            """
            |  Age | Name   | Description                     | Other                   | TheDate     | ADate       |  TheDecimal |   TheDouble |
            | ---: | :----- | :------------------------------ | :---------------------- | :---------- | :---------- | ----------: | ----------: |
            |   12 | Als    | Just some<br/>more things here  | Other { Name = other }  | 01/02/2010  | 02/03/2020  |  123,456.23 |  654,321.32 |
            |   13 | Other  | An anonymous entity             | Other { Name = other }  | 01/02/2010  | 02/03/2020  |    1,234.00 |    4,321.00 |

            """.ReplaceLineEndings()
        );        
    }

    [Test]
    public void WhenRegisteredInAServiceCollectionWithDefaults_ItShouldResolveToTheExpectedConfiguration()
    {
        var provider = new ServiceCollection()
            .AddSingleton<IEnumerableToMarkdownTableBuilderFactory, EnumerableToMarkdownTableBuilderFactory>()
            .AddSingleton(sp => sp
                .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
                .Build())
            .BuildServiceProvider();

        var result = provider.GetRequiredService<IEnumerableToMarkdownTableBuilder>()
            .CreateTable(new Stuff[]
            {
                new(12, "Als", "Just some\r\nmore things here"),
                new(13, "Other", "An anonymous entity") 
            });

        result.Should().Be(
            """
            |  Age | Name   | Description                     | Other                   | TheDate     | ADate                              |  TheDecimal |    TheDouble |
            | ---: | :----- | :------------------------------ | :---------------------- | :---------- | :--------------------------------- | ----------: | -----------: |
            |   12 | Als    | Just some<br/>more things here  | Other { Name = other }  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
            |   13 | Other  | An anonymous entity             | Other { Name = other }  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |

            """.ReplaceLineEndings()
        );        
    }

    [Test]
    public void WhenRegisteredInAServiceCollectionWithDefaultsWIthAnId_ItShouldResolveToTheExpectedConfiguration()
    {
        var provider = new ServiceCollection()
            .AddSingleton<IEnumerableToMarkdownTableBuilderFactory, EnumerableToMarkdownTableBuilderFactory>()
            .AddSingleton(sp => sp
                .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
                .Build())
            .BuildServiceProvider();

        var result = provider.GetRequiredService<IEnumerableToMarkdownTableBuilder>()
            .CreateTable(
            [
                new { Id = 1 },
                new { Id = 2 } 
            ]);

        result.Should().Be(
            """
            |   Id |
            | ---: |
            |    1 |
            |    2 |

            """.ReplaceLineEndings()
        );        
    }    

    [Test]
    public void NumberTransformingTests()
    {
         using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new AllNumbers[]
        {
            new()
        }.ToMarkdownTable();

        result.Should().Be(
            """
            |  IntValue |  ByteValue |  ShortValue |            LongValue |              BigInteger |   Float |  NullDecimal | ADate       |
            | --------: | ---------: | ----------: | -------------------: | ----------------------: | ------: | -----------: | :---------- |
            |       123 |         11 |      12,345 |  123,456,789,012,345 |  12,345,678,901,234,567 |  123.46 |       `null` | 01-10-2010  |
            
            """.ReplaceLineEndings()
        );              
    }

    public class AllNumbers
    {
#pragma warning disable CA1822 // Mark members as static
        public int IntValue => 123;
        public byte ByteValue => 11;
        public short ShortValue => 12345;
        public long LongValue => 123456789012345;
        public BigInteger BigInteger => new(12345678901234567);
        public float Float => 123.456F;
        public decimal? NullDecimal => null;

        [TimeDetectingDateTimeValueTransformer("dd-MM-yyyy", "dd-MM-yyyy")]
        public DateTime ADate => new(2010, 10, 1);
#pragma warning restore CA1822 // Mark members as static
    }

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

    public record Other(string Name);
}
