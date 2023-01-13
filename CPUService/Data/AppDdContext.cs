using CPUMiroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CPUMiroservice.Data
{
    public class AppDdContext : DbContext
    {
        public DbSet<CPU> CPUs { get; set; }

        public AppDdContext(DbContextOptions<AppDdContext> options) : base(options) 
        {
            
        }
    }
}
