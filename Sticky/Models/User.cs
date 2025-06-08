using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sticky.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<ToDoTask> Tasks { get; set; }
    }
}
