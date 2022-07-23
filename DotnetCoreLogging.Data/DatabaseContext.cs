using DotnetCoreLogging.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreLogging.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<LoggerEntity> Loggers { get; set; }

    }
}
