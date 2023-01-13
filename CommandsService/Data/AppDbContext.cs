using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<Command> Commands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CPU>()
                .HasMany(f => f.Commands)
                .WithOne(f => f.CPUs!)
                .HasForeignKey(f => f.CPUId);

            modelBuilder
                .Entity<Command>()
                .HasOne(c => c.CPUs)
                .WithMany(c => c.Commands)
                .HasForeignKey(c => c.CPUId);
        }
    }
}
