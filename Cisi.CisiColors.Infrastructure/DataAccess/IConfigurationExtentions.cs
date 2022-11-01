using Microsoft.Extensions.Configuration;

namespace Cisi.CisiColors.Infrastructure.DB;

internal static class IConfigurationExtentions
{
    public static string GetDatabase(this IConfiguration config, string dbName)
    {
        return config.GetSection("Databases").GetSection(dbName).Value;
    }

    public static string GetDBCollection(this IConfiguration config, string collectionName)
    {
        return config.GetSection("Databases").GetSection("Collections").GetSection(collectionName).Value;
    }
}
