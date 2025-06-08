using Microsoft.AspNetCore.Mvc;
using Sticky.Data;
using Sticky.Models;
using Sticky.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Sticky.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var toDoLists = _context.Tasks
                .Where(t => t.UserId == userId)
                .GroupBy(t => t.Name ?? "")
                .Select(g => new ToDoListDto
                {
                    Name = g.Key,
                    Tasks = g.ToList()
                }).ToList();

            ViewBag.HasTasks = toDoLists.Any();

            var model = new CreateToDoListViewModel
            {
                ToDoLists = toDoLists
            };

            return View(model);
        }

    }
}


