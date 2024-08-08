using System.ComponentModel.DataAnnotations;

namespace Week1.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}
