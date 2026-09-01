using Microsoft.EntityFrameworkCore;
using ResumoCash.Domain.Entities;

namespace ResumoCash.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
}
