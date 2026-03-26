using EventSystemManager.Data.Classes;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext dbContext;

        public EventService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await dbContext.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await dbContext.Events
                .Include(e => e.Category)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Event model)
        {
            await dbContext.Events.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ev = await dbContext.Events.FindAsync(id);
            if (ev != null)
            {
                dbContext.Events.Remove(ev);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            return await dbContext.Venues.ToListAsync();
        }
    }
}