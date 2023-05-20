using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Models.Request;

public class TopicRequestModel
{
    [Required]
    public string Name { get; set; }
}
