using Microsoft.EntityFrameworkCore;

namespace Docker_Tests.Domain;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    public DbSet<Person> People { get; set; }
}
