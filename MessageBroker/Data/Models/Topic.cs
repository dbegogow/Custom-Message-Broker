using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Data.Models;

public class Topic
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Name { get; set; }
}
