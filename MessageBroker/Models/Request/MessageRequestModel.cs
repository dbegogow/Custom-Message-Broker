namespace MessageBroker.Models.Request;

public class MessageRequestModel
{
    public string TopicMessage { get; init; }

    public int SubscriptionId { get; init; }

    public DateTime ExpiresAfter { get; init; }

    public string MessageStatus { get; init; }
}
