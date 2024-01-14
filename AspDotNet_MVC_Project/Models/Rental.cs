using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "Total Price")]
        [Column(TypeName = "int")]
        public int TotalPrice { get; set; }
        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }
        public User Customer { get; set; }
        [ForeignKey("Car")]
        [Display(Name = "Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
