using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DentistBackend.WebApi.PlayerDbContext;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<PlayerStats> PlayerStats { get; set; }
    public DbSet<User> Users { get; set; }
}
