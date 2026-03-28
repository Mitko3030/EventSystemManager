using EventSystemManager.Data.Classes;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Services
{
    public class ParticipantService
    {
        private readonly ApplicationDbContext dbContext;

        public ParticipantService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Participant>> GetAllAsync()
        {
            return await dbContext.Participants.ToListAsync();
        }

        public async Task AddAsync(Participant model)
        {
            await dbContext.Participants.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var participant = await dbContext.Participants.FindAsync(id);
            if (participant != null)
            {
                dbContext.Participants.Remove(participant);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}