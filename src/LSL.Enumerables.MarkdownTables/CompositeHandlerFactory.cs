using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.Enumerables.MarkdownTables;

internal static class CompositeHandlerFactory
{
    public static BuildCompositeHandlerResult<TContext, TResult> CreateCompositeHandler<TContext, TResult>(
        IEnumerable<HandlerDelegate<TContext, TResult>> handlers)
    {
        return new BuildCompositeHandlerResult<TContext, TResult>(
            handlers
                .Reverse()
                .Aggregate(
                    new Func<TContext, TResult>(_ => default),
                    (compositeFunction, currentFn) => context => currentFn(context, () => compositeFunction(context))
                )
        );
    }
}
