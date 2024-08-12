using System.ComponentModel.DataAnnotations;

namespace Week1.ViewModel
{
    public class ToDoItemVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}
