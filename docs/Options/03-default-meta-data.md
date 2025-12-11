# Default meta data provider

The default meta data provider can be configured with extra options such as excluding specific properties as below:

## Exclude properties by name

```csharp { data-fiddle="wfDYsb" }
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1")
}
.ToMarkdownTable(c => c
    .UseDefaultPropertyMetaDataProvider(c => c.ExcludeProperties("Value")));

/* result will be
| Key   |
| :---- |
| Key1  |
*/
```

!!! info
    Multiple properties can be passed into `ExcludeProperties`

## Exclude properties by expression

```csharp { data-fiddle="Qz9xqA" }
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1")
}
.ToMarkdownTable(c => c
    .UseDefaultPropertyMetaDataProvider(c => c
        .ForClass<KeyValuePair<string, string>>(c => c
            .ExcludeProperties((p => p.Key)))));

/* result will be
| Value   |
| :------ |
| Value1  |
*/
```

!!! info
    Multiple properties can be passed into `ExcludeProperties`

## Include properties by name

```csharp { data-fiddle="uoxP30" }
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1")
}
.ToMarkdownTable(c => c
    .UseDefaultPropertyMetaDataProvider(c => c.IncludeProperties("Value")));

/* result will be
| Value   |
| :------ |
| Value1  |
*/
```
!!! info
    Multiple properties can be passed into `IncludeProperties`

## Include properties by expression

```csharp { data-fiddle="0gDWN0" }
var result = new List<KeyValuePair<string, string>>
{
    new("Key1", "Value1")
}
.ToMarkdownTable(c => c
    .UseDefaultPropertyMetaDataProvider(c => c
        .ForClass<KeyValuePair<string, string>>(c => c
            .IncludeProperties((p => p.Key)))));

/* result will be
| Value   |
| :------ |
| Value1  |
```

!!! info
    Multiple properties can be passed into `IncludeProperties`