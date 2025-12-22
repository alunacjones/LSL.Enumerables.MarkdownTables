#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Services enumerable to markdown table builder interface
/// </summary>
public interface IServicesEnumerableToMarkdownTableBuilder
{
    /// <summary>
    /// The original service collection
    /// </summary>
    public IServiceCollection Services { get; }
}
