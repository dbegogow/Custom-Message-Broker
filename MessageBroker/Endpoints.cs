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

            endpoints.MapPost("api/topics/{id}/messages", async (
                AppDbContext data,
                int id,
                MessageRequestModel message) =>
            {
                var isTopicExist = await data.Topics
                    .AnyAsync(t => t.Id == id);

                if (!isTopicExist)
                    return Results.NotFound("Topic not found");

                var subs = await data.Subscriptions
                    .Where(s => s.TopicId == id)
                    .ToListAsync();

                if (subs.Count == 0)
                    return Results.NotFound("There are no subscriptions for this topic");

                foreach (var sub in subs)
                {
                    var newMessage = new Message
                    {
                        TopicMessage = message.TopicMessage,
                        SubscriptionId = sub.Id,
                        ExpiresAfter = message.ExpiresAfter,
                        MessageStatus = message.MessageStatus
                    };

                    await data.Messages.AddAsync(newMessage);
                }

                await data.SaveChangesAsync();

                return Results.Ok("Message has been published");
            });
        });
}
