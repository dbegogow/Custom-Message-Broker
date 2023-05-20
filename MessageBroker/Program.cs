using MessageBroker;
using MessageBroker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddDatabase(configuration)
    .AddRequestModelsValidators();

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseRouting()
    .RegisterEndpoints();

app.Run();