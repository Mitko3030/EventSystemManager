using EventSystemManager.Data.Classes;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Services
{
    public class VenueService
    {
        private readonly ApplicationDbContext dbContext;

        public VenueService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Venue>> GetAllAsync()
        {
            return await dbContext.Venues.ToListAsync();
        }

        public async Task AddAsync(Venue model)
        {
            await dbContext.Venues.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var venue = await dbContext.Venues.FindAsync(id);
            if (venue != null)
            {
                dbContext.Venues.Remove(venue);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}