using MessageBroker.Data;
using MessageBroker.Data.Models;
using MessageBroker.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker;

public static class Endpoints
{
    public static IApplicationBuilder RegisterEndpoints(this IApplicationBuilder builder)
        => builder.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("api/topics", async (AppDbContext data, TopicRequestModel topic) =>
            {
                var newTopic = new Topic { Name = topic.Name };

                await data.Topics.AddAsync(newTopic);
                await data.SaveChangesAsync();

                return Results.Created($"api/topics/{newTopic.Id}", newTopic);
            });

            endpoints.MapGet("api/topics", async (AppDbContext data) =>
                Results.Ok(await data.Topics.ToListAsync()));
        });
}
