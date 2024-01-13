using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int CarId { get; set; }
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RentalDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReturnDate { get; set; }
        [Column(TypeName = "int")]
        public int TotalPrice { get; set; }
    }
}
