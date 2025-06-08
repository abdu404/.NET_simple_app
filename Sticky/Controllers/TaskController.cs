using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sticky.Data;
using Sticky.Models;
using Sticky.Models.ViewModels;
using System.Security.Claims;

[Authorize]
public class TaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Create(string Name, List<string> Descriptions)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        foreach (var description in Descriptions.Where(d => !string.IsNullOrWhiteSpace(d)))
        {
            var task = new ToDoTask
            {
                UserId = userId,
                Name = Name,
                Description = description,
                IsCompleted = false
            };
            _context.Tasks.Add(task);
        }

        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
    public IActionResult DeleteList(string groupName)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var tasks = _context.Tasks
            .Where(t => t.UserId == userId && (t.Name ?? "") == (groupName ?? ""))
            .ToList();

        _context.Tasks.RemoveRange(tasks);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleComplete(int taskId, bool isCompleted)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task != null)
        {
            task.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

}


