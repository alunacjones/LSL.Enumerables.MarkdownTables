# DateTime Extensions

There are extension methods available to check if a `DateTime` or `DateTimeOffset` are deemed date only.

Other methods are also provided for formatting. These methods are used by the `TimeDetectingDateTimeValueTransformer`

## IsDateOnly

Checks if a `DateTime` or `DateTimeOffset` is date only i.e. the time values are all zero.

```csharp { data-fiddle="8ubGRu" }
var isDateOnly = new DateTime(2010, 1, 1).IsDateOnly();
var isDateOnly2 = new DateTime(2010, 1, 1, 13, 0, 0).IsDateOnly();

/*
    isDateOnly will be true
    isDateOnly2 will be false
*/
```

## ToStringWithEmptyTimeDetection

Returns a string representation of a `DateTme` or `DateTimeOffset` using the given date formats.

If the source date time value has no time component then the `dateOnlyFormat` will be used to generate the string, otherwise the `dateTimeFormat` will be used.

```csharp { data-fiddle="yftgoa" }
var first = new DateTime(2010, 1, 1)
    .ToStringWithEmptyTimeDetection("G", "d");

var second = new DateTime(2010, 1, 1, 13, 0, 0)
    .ToStringWithEmptyTimeDetection("G", "d");

/*
    first will be 01/01/2010
    second will be 01/01/2010 13:00:00
*/
```