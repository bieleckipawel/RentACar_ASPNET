using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AspDotNet_MVC_Project.Models
{
    public class User: IdentityUser
    {
        [Column(TypeName = "varchar(50)")]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "Is Verified?")]
        public bool IsVerified { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class AdminUserConfig
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
