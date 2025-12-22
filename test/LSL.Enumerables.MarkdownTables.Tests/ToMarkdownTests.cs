using System;
using System.Collections.Generic;
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
        }.ToMarkdownTable(c => c
            .UseEmptyResult("nope")
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
        }.ToMarkdownTable(c => c
            .UseEmptyResult("nowt")
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
            .ToMarkdownTable(c => c.UseEmptyResult("nowt"))
            .Should().Be("nowt");
    }
    
    [Test]
    public void WhenRegisteredInAServiceCollection_ItShouldResolveToTheExpectedConfiguration()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var provider = new ServiceCollection()
            .AddSingleton<IEnumerableToMarkdownTableBuilderFactory, EnumerableToMarkdownTableBuilderFactory>()
            .AddSingleton(sp => sp
                .GetRequiredService<IEnumerableToMarkdownTableBuilderFactory>()
                .Build(c => c
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
        using var disposableCulture = new DisposableBritishCultureInfo();

        var provider = new ServiceCollection()
            .AddEnumerablesToMarkdown()
            .Services
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
    public void WhenStronglyTypedRegisteredInAServiceCollectionWithOverriddenDefaults_ItShouldResolveToTheExpectedConfiguration()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var provider = new ServiceCollection()
            .AddEnumerablesToMarkdown()
            .AddBuilder<Stuff>(c => c.UseDefaultPropertyMetaDataProvider(c => c
                .ExcludeProperties(x => x.Description)
                .IncludeProperties(x => x.Name, x => x.Age)))
            .Services
            .BuildServiceProvider();

        var result = provider.GetRequiredService<IEnumerableToMarkdownTableBuilder<Stuff>>()
            .CreateTable(
            [
                new(12, "Als", "Just some\r\nmore things here"),
                new(13, "Other", "An anonymous entity") 
            ]);

        result.Should().Be(
            """
            |  Age | Name   |
            | ---: | :----- |
            |   12 | Als    |
            |   13 | Other  |

            """.ReplaceLineEndings()
        );        
    }

    [Test]
    public void WhenStronglyTypedRegisteredInAServiceCollectionWithDefaults_ItShouldResolveToTheExpectedConfiguration()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var provider = new ServiceCollection()
            .AddEnumerablesToMarkdown()
            .AddBuilder<Stuff>()
            .Services
            .BuildServiceProvider();

        var result = provider.GetRequiredService<IEnumerableToMarkdownTableBuilder<Stuff>>()
            .CreateTable(
            [
                new(12, "Als", "Just some\r\nmore things here"),
                new(13, "Other", "An anonymous entity") 
            ]);

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
    public void FactoryInstance_ShouldNotBeNull()
    {
        EnumerableToMarkdownTableBuilderFactory.Instance.Should().NotBeNull();
    }

    [Test]
    public void WhenRegisteredInAServiceCollectionWithDefaultsWIthAnId_ItShouldResolveToTheExpectedConfiguration()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var provider = new ServiceCollection()
            .AddEnumerablesToMarkdown()
            .Services
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

    [Test]
    public void UsingDefaultsWithACustomisedDefaultMetaDataProviderExcludingFields_ShouldProduceTheExpectedResult()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new InclusionAndExclusion[]
        {
            new(1, "a-name", "a-description")
        }.ToMarkdownTable(new EnumerableToMarkdownTableBuilderOptions()
            .UseDefaultPropertyMetaDataProvider(c => c
                .UseHeaderProvider(pi => $"*{pi.Name}*")
                .ForClass<InclusionAndExclusion>(c => c
                    .ExcludeProperties(c => c.Description))
            )
        );

        result.Should().Be(
            """
            |  *Id* | *Name*  |
            | ----: | :------ |
            |     1 | a-name  |
            
            """.ReplaceLineEndings()
        );        
    }

    [Test]
    public void UsingDefaultsWithACustomisedDefaultMetaDataProviderIncludingFields_ShouldProduceTheExpectedResult()
    {
        using var disposableCulture = new DisposableBritishCultureInfo();

        var result = new InclusionAndExclusion[]
        {
            new(1, "a-name", "a-description")
        }.ToMarkdownTable(c => c
            .UseDefaultPropertyMetaDataProvider(c => c
                .IncludeProperties(nameof(InclusionAndExclusion.Name))
                .ExcludeProperties(nameof(InclusionAndExclusion.Description))
                .ForClass<InclusionAndExclusion>(c => c
                    .IncludeProperties(c => c.Id, c => c.Description))
            )
        );

        result.Should().Be(
            """
            |   Id | Name    |
            | ---: | :------ |
            |    1 | a-name  |
            
            """.ReplaceLineEndings()
        );        
    }
}
