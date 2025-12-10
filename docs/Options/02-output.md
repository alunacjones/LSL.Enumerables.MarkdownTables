# Empty enumerable output

The following code overrides the default of returning `null` to return `No Data`

```csharp { data-fiddle="zZup48" }
var result = new List<KeyValuePair<string, string>>()
    .ToMarkdownTable(c => c.UseEmptyResult("No Data"));

/* result will be: No Data */
```