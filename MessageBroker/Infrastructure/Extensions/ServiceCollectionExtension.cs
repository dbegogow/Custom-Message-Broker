using MessageBroker.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetDefaultConnectionString()));
}
