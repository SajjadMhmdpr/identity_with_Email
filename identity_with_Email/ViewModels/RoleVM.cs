using System.ComponentModel.DataAnnotations;

namespace identity_with_Email.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
