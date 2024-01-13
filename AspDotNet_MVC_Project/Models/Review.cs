using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int CarId { get; set; }
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
