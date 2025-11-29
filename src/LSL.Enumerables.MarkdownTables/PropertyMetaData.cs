using System;
using System.Reflection;

namespace LSL.Enumerables.MarkdownTables;

public class PropertyMetaData(PropertyInfo propertyInfo, bool includeInOutput = true, Justification justification = Justification.Left)
{
    public bool IncludeInOutput { get; } = includeInOutput;
    public Justification Justification { get; } =  justification;
    public PropertyInfo PropertyInfo { get; } = propertyInfo;
    public IValueTransformer ValueTransformer { get; set; }
}
