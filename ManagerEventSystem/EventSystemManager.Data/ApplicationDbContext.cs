using EventSystemManager.Data;
using EventSystemManager.Data.Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerEventSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Venue> Venues { get; set; }
    }
}
