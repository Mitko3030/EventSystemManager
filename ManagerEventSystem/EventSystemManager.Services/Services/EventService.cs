using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystemManager.Data.Classes;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Services.Controllers
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
            return await dbContext.Events.ToListAsync();
        }

        public async Task AddAsync(Event model)
        {
            await dbContext.Events.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

    }
}
