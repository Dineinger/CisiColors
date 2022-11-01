using Cisi.CisiColors.Infrastructure.DB;
using Microsoft.Extensions.DependencyInjection;

namespace Cisi.CisiColors.Infrastructure;

public static class IServiceCollectionExtentions
{
    public static IServiceCollection AddCisiColorDatabaseServices(this IServiceCollection services)
    {
        services.AddSingleton<IColorDataAccess, ColorDataAccess>();
        return services;
    }
}
