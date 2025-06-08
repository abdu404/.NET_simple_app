using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sticky.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        // Optional task name
        public string? Name { get; set; }

        [Required(ErrorMessage = "Task description is required.")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        // Foreign key to User
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
