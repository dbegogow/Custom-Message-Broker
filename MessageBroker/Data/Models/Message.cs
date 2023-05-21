using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Data.Models;

public class Message
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string TopicMessage { get; set; }

    public int SubscriptionId { get; set; }

    [Required]
    public DateTime ExpiresAfter { get; set; } = DateTime.Now.AddDays(1);

    [Required]
    public string MessageStatus { get; set; } = "NEW";
}
