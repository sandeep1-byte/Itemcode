using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Item_Code_management_System.Models
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string ItemCode { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(20)]
        public string Unit { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; } // Foreign key for item category
        public IEnumerable<SelectListItem>? Categories { get; set; } // For dropdown population
    }
}
