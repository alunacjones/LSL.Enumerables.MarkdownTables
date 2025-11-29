[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-enumerables-markdowntables.svg)](https://ci.appveyor.com/project/alunacjones/lsl-enumerables-markdowntables)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Enumerables.MarkdownTables)](https://coveralls.io/github/alunacjones/LSL.Enumerables.MarkdownTables)
[![NuGet](https://img.shields.io/nuget/v/LSL.Enumerables.MarkdownTables.svg)](https://www.nuget.org/packages/LSL.Enumerables.MarkdownTables/)

# LSL.Enumerables.MarkdownTables

This library provides methods to generate a markdown table from a strongly typed `IEnumerable<T>`

The following example uses all the defaults:

```csharp
var result = new Stuff[]
{
    new(12, "Als", "Just some\r\nmore things here"),
    new(13, "Other", "An anonymous entity") 
}.ToMarkdownTable();

/* result will contain:

|  Age | Name   | Description                     | TheDate     | ADate                              |  TheDecimal |    TheDouble |
| ---: | :----- | :------------------------------ | :---------- | :--------------------------------- | ----------: | -----------: |
|   12 | Als    | Just some<br/>more things here  | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |      123456 |  £654,321.32 |
|   13 | Other  | An anonymous entity             | 01/02/2010  | 2020-03-02T00:00:00.0000000+00:00  |        1234 |    £4,321.00 |
*/
```

