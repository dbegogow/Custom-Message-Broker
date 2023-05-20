using MessageBroker.Data;
using MessageBroker.Data.Models;
using MessageBroker.Models.Request;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace MessageBroker;

public static class Endpoints
{
    public static IApplicationBuilder RegisterEndpoints(this IApplicationBuilder builder)
        => builder.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("api/topics", async (
                AppDbContext data,
                IValidator<TopicRequestModel> validator,
                TopicRequestModel topic) =>
            {
                var validationResult = await validator.ValidateAsync(topic);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var newTopic = new Topic { Name = topic.Name };

                await data.Topics.AddAsync(newTopic);
                await data.SaveChangesAsync();

                return Results.Created($"api/topics/{newTopic.Id}", newTopic);
            });

            endpoints.MapGet("api/topics", async (AppDbContext data) =>
                Results.Ok(await data.Topics.ToListAsync()));

            endpoints.MapPost("api/topics/{id}/messages", async (
                AppDbContext data,
                IValidator<MessageRequestModel> validator,
                int id,
                MessageRequestModel message) =>
            {
                var validationResult = await validator.ValidateAsync(message);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

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
                        SubscriptionId = sub.Id
                    };

                    await data.Messages.AddAsync(newMessage);
                }

                await data.SaveChangesAsync();

                return Results.Ok("Message has been published");
            });

            endpoints.MapPost("api/topics/{id}/subscriptions", async (
                AppDbContext data,
                IValidator<SubscriptionRequestModel> validator,
                int id,
                SubscriptionRequestModel sub) =>
            {
                var validationResult = await validator.ValidateAsync(sub);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var isTopicExist = await data.Topics
                    .AnyAsync(t => t.Id == id);

                if (!isTopicExist)
                    return Results.NotFound("Topic not found");

                var newSub = new Subscription
                {
                    Name = sub.Name,
                    TopicId = id
                };

                await data.Subscriptions.AddAsync(newSub);
                await data.SaveChangesAsync();

                return Results.Created($"api/topics/{id}/subscriptions/{newSub.Id}", newSub);
            });
        });
}
