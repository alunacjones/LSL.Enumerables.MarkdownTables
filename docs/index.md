[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-enumerables-markdowntables.svg)](https://ci.appveyor.com/project/alunacjones/lsl-enumerables-markdowntables)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Enumerables.MarkdownTables)](https://coveralls.io/github/alunacjones/LSL.Enumerables.MarkdownTables)
[![NuGet](https://img.shields.io/nuget/v/LSL.Enumerables.MarkdownTables.svg)](https://www.nuget.org/packages/LSL.Enumerables.MarkdownTables/)

# LSL.Enumerables.MarkdownTables

This library provides methods to generate a markdown table from a strongly typed `IEnumerable<T>`

The following example uses all the defaults:

```csharp { data-fiddle="zZup48" }
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1"),
    new("Key2", "Value2"),
    new("Key3", null)
}.ToMarkdownTable();

/* result will contain:

| Key   | Value   |
| :---- | :------ |
| Key1  | Value1  |
| Key2  | Value2  |
| Key3  | `null`  |
*/
```

## Defaults

* All top-level public properties that are simple types are output
* Numbers are justified to the right.
* Header names are the names of each property
* `DateTime` and `DateTimeOffset` properties use the `G` format string (e.g. en-GB would show `20/02/2020 23:00:00` for 20th Feb 2020 at 11PM)
    * If there is no time portion then the `d` format string is used to only output the date.
* Numeric data types will use the `N2` format string (e.g. en-GB would show `2,123.22` for a decimal value of `2123.22`)
    * all intrinsic integer values will use the `N0` format string
* `null` values will be output as `` `null` ``

