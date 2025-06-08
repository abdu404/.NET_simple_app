using Microsoft.EntityFrameworkCore;
using Sticky.Models;

namespace Sticky.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
