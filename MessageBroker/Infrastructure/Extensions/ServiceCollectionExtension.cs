using MessageBroker.Data;
using MessageBroker.Models.Request;
using MessageBroker.Models.Request.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace MessageBroker.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetDefaultConnectionString()));

    public static IServiceCollection AddRequestModelsValidators(this IServiceCollection services)
            => services
                .AddScoped<IValidator<TopicRequestModel>, TopicRequestModelValidator>()
                .AddScoped<IValidator<MessageRequestModel>, MessageRequestModelValidator>();
}
