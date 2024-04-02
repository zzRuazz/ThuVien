using Common.Abstractions;
using Common.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

/// <summary>
/// ProviderRegister
/// </summary>
public static class CommonRegister
{
    /// <summary>
    /// AddProviderServices
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterProviders(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddTransient<IAdminApiProvider, AdminApiProvider>();
        services.AddTransient<IConfigProvider, ConfigProvider>();
        services.AddTransient<ICookieProvider, CookieProvider>();
        services.AddTransient<ICoreApiProvider, CoreApiProvider>();
        services.AddTransient<ICustomerApiProvider, CustomerApiProvider>();
        services.AddTransient<IHashProvider, HashProvider>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<ILogProvider, LogProvider>();
        services.AddTransient<IMemCacheProvider, MemCacheProvider>();
        services.AddTransient<IRestProvider, RestProvider>();
        services.AddTransient<IStringProvider, StringProvider>();
    }
}
