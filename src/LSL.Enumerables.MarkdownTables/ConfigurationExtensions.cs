using System;

namespace LSL.Enumerables.MarkdownTables;

internal static class ConfigurationExtensions
{
    public static T ConfigureOptions<T>(this Action<T> source, T options)
        where T : class
    {
        if (source is null) return null;

        source(options);

        return options;
    }
}
