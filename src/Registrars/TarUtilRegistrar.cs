using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Compression.Tar.Abstract;

namespace Soenneker.Compression.Tar.Registrars;

/// <summary>
/// A utility library dealing with Tar compression and decompression
/// </summary>
public static class TarUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ITarUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTarUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<ITarUtil, TarUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ITarUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTarUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ITarUtil, TarUtil>();

        return services;
    }
}
