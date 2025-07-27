using Microsoft.EntityFrameworkCore;
using RegistrationTemplate.Models;

namespace RegistrationTemplate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RegistrationSession> RegistrationSessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Gender to be stored as string
            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
