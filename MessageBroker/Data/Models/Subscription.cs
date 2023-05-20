using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Data.Models;

public class Subscription
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int TopicId { get; set; }
}
