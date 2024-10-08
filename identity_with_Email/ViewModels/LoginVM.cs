using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace identity_with_Email.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(200)]
        public string UserName { get; set; }
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
        [AllowNull]
        public ExternalLogin ExternalLogin { get; set; }
    }
    public class ExternalLogin
    {
        public string ReturnUrl { get; set; }
        public List<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
