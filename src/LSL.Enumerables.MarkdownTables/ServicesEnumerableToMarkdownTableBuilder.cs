#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

internal class ServicesEnumerableToMarkdownTableBuilder(IServiceCollection services) : IServicesEnumerableToMarkdownTableBuilder
{
    public IServiceCollection Services => services;
}
