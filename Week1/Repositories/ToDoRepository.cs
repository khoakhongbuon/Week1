using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Week1.Models;
using Week1.Repositories;

namespace Week1.Data
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(string userId)
        {
            return await _context.ToDoItems
                .Where(item => item.UserId == userId)
                .ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id, string userId)
        {
            return await _context.ToDoItems
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        }

        public async Task AddAsync(ToDoItem item)
        {
            await _context.ToDoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var item = await _context.ToDoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
