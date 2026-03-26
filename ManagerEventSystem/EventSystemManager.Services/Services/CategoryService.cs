using EventSystemManager.Data;
using EventSystemManager.Data.Classes;
using ManagerEventSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSystemManager.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await dbContext.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category model)
        {
            await dbContext.Categories.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category model)
        {
            dbContext.Categories.Update(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}