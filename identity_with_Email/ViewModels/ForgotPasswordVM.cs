using System.ComponentModel.DataAnnotations;

namespace identity_with_Email.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
