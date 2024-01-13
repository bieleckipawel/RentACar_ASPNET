using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AspDotNet_MVC_Project.Models
{
    public class User: IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        [RegularExpression("^[\\p{L}\\s'-]+$", ErrorMessage = "Provide full name!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n",ErrorMessage = "Invalid mail address!")]
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "int")]
        public int Type { get; set; } // 2 = admin, 1 = customer
        [Column(TypeName = "bit")]
        public bool IsVerified { get; set; }
    }
}
