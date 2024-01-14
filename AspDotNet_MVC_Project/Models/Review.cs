using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    /// <summary>
    /// not used. i have more than 4 tables in my database - cars, customers, rentals, roles etc. i only need 4 tables according to specification.
    /// </summary>
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
