using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Week1.Models;
using Week1.Repositories;
using Week1.ViewModel;

namespace Week1.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _repository;
        private readonly UserManager<AppUser> _userManager;

        public ToDoController(IToDoRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // GET: /ToDo
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _repository.GetAllAsync(userId);
            return View("Index", items);

        }

        // GET: /ToDo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItemVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoItem = new ToDoItem
                {
                    Title = model.Title,
                    Description = model.Description,
                    IsCompleted = model.IsCompleted,
                    UserId = userId
                };
                await _repository.AddAsync(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /ToDo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var toDoItem = await _repository.GetByIdAsync(id, userId);
            if (toDoItem == null)
            {
                return NotFound();
            }
            var model = new ToDoItemVM
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                IsCompleted = toDoItem.IsCompleted
            };
            return View(model);
        }

        // POST: /ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ToDoItemVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoItem = await _repository.GetByIdAsync(model.Id, userId);
                if (toDoItem == null)
                {
                    return NotFound();
                }
                toDoItem.Title = model.Title;
                toDoItem.Description = model.Description;
                toDoItem.IsCompleted = model.IsCompleted;
                await _repository.UpdateAsync(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /ToDo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var toDoItem = await _repository.GetByIdAsync(id, userId);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // POST: /ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _repository.DeleteAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
