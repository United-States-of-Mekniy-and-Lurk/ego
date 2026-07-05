using Microsoft.Extensions.DependencyInjection;

namespace Ego.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services;
    }
}
