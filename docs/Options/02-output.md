# Empty enumerable output

The following code overrides the default of returning `null` to return `No Data`

```csharp { data-fiddle="24hDuk" }
var result = new List<KeyValuePair<string, string>>()
    .ToMarkdownTable(c => c.UseEmptyResult("No Data"));

/* result will be: No Data */
```