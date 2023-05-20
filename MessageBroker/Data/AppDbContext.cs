using MessageBroker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageBroker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Topic> Topics { get; init; }

    public DbSet<Subscription> Subscriptions { get; init; }

    public DbSet<Message> Messages { get; init; }
}
