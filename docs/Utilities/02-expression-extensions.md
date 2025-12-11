# Expression Extensions

There are extension methods available to get the name or `PropertyInfo` of a property from an expression.

## GetPropertyInfo

Returns the `PropertyInfo` of the property specified by the source expression

```csharp { data-fiddle="R5xlOG" }
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static PropertyInfo GetPropertyInfo(
        Expression<Func<MyClass, object>> expression)
    {
        return expression.GetPropertyInfo();
    }
}

var propertyInfo = MyClass.GetPropertyInfo(c => c.Name);
/*
    propertyInfo will contain the PropertyInfo instance for the "Name" property
*/
```

## GetPropertyName

Returns the property name of the property specified by the source expression

```csharp { data-fiddle="Mb4fzC" }
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static string GetPropertyName(
        Expression<Func<MyClass, object>> expression)
    {
        return expression.GetPropertyName();
    }
}

var propertyName = MyClass.GetPropertyInfo(c => c.Name);
/*
    propertyName will be "Name"
*/
```