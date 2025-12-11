# PropertyInfo Extensions

There are extension methods available to reuse existing behviour when defining custom property meta data providers.

## GetJustification

Get's the `Justification` of a property based on its type. Any numeric type will be justified right. All other types will be justified left.

```csharp { data-fiddle="jBHrU4" }
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }
}

var idJustification = typeof(MyClass).GetProperty("Id").GetJustification();
var nameJustification = typeof(MyClass).GetProperty("Name").GetJustification();

/*
    idJustification will be Justification.Right
    nameJustification will be Justification.Left
*/
```

## IsSimpleType

Returns `true` if the property is deemed to be a simple type.

The property type is considered simple if one of the following is true:

1. Is a primitive
2. Is a value type
3. Is an enum
4. Is a `string`
5. Is a `decimal`

```csharp { data-fiddle="p8GFEC" }
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }
}

var idIsSimple = typeof(MyClass).GetProperty("Id").IsSimpleType();
var nameIsSimple = typeof(MyClass).GetProperty("Name").IsSimpleType();

/*
    idIsSimple will be true
    nameIsSimple will be true
*/

```

## ResolveValueTransformerFromAttributes

Mainly used internally, but has been exposed to allow custom meta data providers to re-use the following attributes:

| Attribute                                      | Notes                                                      |
| ---------------------------------------------- | ---------------------------------------------------------- |
| NumberValueTransformerAttribute                | Applies a number value transformer to the property         |
| DateTimeValueTransformerAttribute              | Applies a date time value transformer to the property      |
| TimeDetectingDateTimeValueTransformerAttribute | Applies a time detecting value transformer to the property |

!!! warning
    Application of a value transformer to a property takes precedence over the global defaults for your builder.

## ResolveOutputAllowedFromAttributesAndType

Returns a value indicating as to whether a property should be outputted.

It initially checks the type is a simple type but can be overridden with the application of the `IncludeInOutputAttribute`.
