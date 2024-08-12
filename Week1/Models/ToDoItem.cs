using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week1.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        [ForeignKey("AppUser")]
        public string? UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
