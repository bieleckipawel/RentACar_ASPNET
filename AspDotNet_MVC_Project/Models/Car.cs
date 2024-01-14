using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNet_MVC_Project.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Make { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Model { get; set; }
        [Column(TypeName = "int")]
        public int MfYear { get; set; }
        [Column(TypeName = "int")]
        public int Price { get; set; }

        [Column(TypeName = "bit")] 
        public bool Available { get; set; } = true;
    }
}
