using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Week1.Data;
using Week1.Models;

namespace Week1.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(string userId, string searchTerm = "")
        {
            var query = _context.ToDoItems.Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm) || t.Description.Contains(searchTerm));
            }

            return await query.ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id, string userId)
        {
            return await _context.ToDoItems
                .Where(t => t.Id == id && t.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var item = await GetByIdAsync(id, userId);
            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
