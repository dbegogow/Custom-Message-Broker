namespace Subscriber.Models;

public class MessageReadModel
{
    public int Id { get; init; }

    public string TopicMessage { get; init; }

    public DateTime ExpiresAfter { get; init; }

    public string MessageStatus { get; init; }
}
