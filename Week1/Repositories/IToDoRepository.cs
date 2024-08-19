using System.Collections.Generic;
using System.Threading.Tasks;
using Week1.Models;

namespace Week1.Repositories
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync(string userId, string searchTerm = "");
        Task<ToDoItem?> GetByIdAsync(int id, string userId);
        Task AddAsync(ToDoItem item);
        Task UpdateAsync(ToDoItem item);
        Task DeleteAsync(int id, string userId);
    }
}
