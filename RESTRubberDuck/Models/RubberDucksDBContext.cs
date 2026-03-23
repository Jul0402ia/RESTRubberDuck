namespace RESTRubberDuck.Models;
using Microsoft.EntityFrameworkCore;

public class RubberDucksDBContext : DbContext
{
    public RubberDucksDBContext(DbContextOptions<RubberDucksDBContext> options)
        : base(options)
    {
    }

    public DbSet<RubberDuck> RubberDucks { get; set; }
}