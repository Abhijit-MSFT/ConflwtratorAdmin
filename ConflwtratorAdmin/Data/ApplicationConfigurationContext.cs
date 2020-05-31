using Microsoft.EntityFrameworkCore;
using ConflwtratorAdmin.Models;

namespace ConflwtratorAdmin.Data
{
    public class ApplicationConfigurationContext : DbContext
    {

        public ApplicationConfigurationContext(DbContextOptions<ApplicationConfigurationContext> options) : base(options)
        {
        }

        public DbSet<ApplicationsConfiguration> AppConfiguration { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationsConfiguration>().ToTable("ApplicationsConfiguration");
        }

    }
}
