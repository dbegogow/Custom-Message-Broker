using MessageBroker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddDatabase(configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();