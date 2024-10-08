using System.ComponentModel.DataAnnotations;

namespace identity_with_Email.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
