[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-enumerables-markdowntables.svg)](https://ci.appveyor.com/project/alunacjones/lsl-enumerables-markdowntables)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Enumerables.MarkdownTables)](https://coveralls.io/github/alunacjones/LSL.Enumerables.MarkdownTables)
[![NuGet](https://img.shields.io/nuget/v/LSL.Enumerables.MarkdownTables.svg)](https://www.nuget.org/packages/LSL.Enumerables.MarkdownTables/)

# LSL.Enumerables.MarkdownTables

This library provides methods to generate a markdown table from a strongly typed `IEnumerable<T>`

The following example uses all the defaults:

```csharp <!-- REPLACE -->.*<!-- END:REPLACE -->
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1"),
    new("Key2", "Value2")
}.ToMarkdownTable();

/* result will contain:

| Key   | Value   |
| :---- | :------ |
| Key1  | Value1  |
| Key2  | Value2  |
*/
```

<!-- HIDE -->
## Further Documentation

More in-depth documentation can be found [here](https://alunacjones.github.io/LSL.Enumerables.MarkdownTables/)
<!-- END:HIDE -->