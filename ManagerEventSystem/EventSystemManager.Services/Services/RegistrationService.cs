using EventSystemManager.Data;
using EventSystemManager.Data.Classes;
using Microsoft.EntityFrameworkCore;
using ManagerEventSystem.Data;

namespace EventSystemManager.Services
{
    public class RegistrationService
    {
        private readonly ApplicationDbContext dbContext;

        public RegistrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Registration>> GetAllAsync()
        {
            return await dbContext.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .ToListAsync();
        }

        public async Task AddAsync(Registration model)
        {
            await dbContext.Registrations.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Registration?> GetByIdAsync(int id)
        {
            return await dbContext.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var registration = await dbContext.Registrations.FindAsync(id);
            if (registration != null)
            {
                dbContext.Registrations.Remove(registration);
                await dbContext.SaveChangesAsync();
            }
        }

        // NEW: Get all events for the dropdown in Create Registration form
        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        // NEW: Get all participants for the dropdown in Create Registration form
        public async Task<List<Participant>> GetAllParticipantsAsync()
        {
            return await dbContext.Participants.ToListAsync();
        }
    }
}