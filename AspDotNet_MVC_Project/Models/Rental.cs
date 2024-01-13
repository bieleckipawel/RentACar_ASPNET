using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int CarId { get; set; }
        [Required]
        [Column(TypeName = "string")]
        public string CustomerId { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Return Date")]
        [Compare("RentalDate", ErrorMessage = "Return Date must be greater than Rental Date")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "Total Price")]
        [Column(TypeName = "int")]
        public int TotalPrice { get; set; }
        [ForeignKey("CustomerId")]
        [Display(Name = "Customer")]
        public User Customer { get; set; }
        [ForeignKey("CarId")]
        [Display(Name = "Car")]
        public Car Car { get; set; }
    }
}
