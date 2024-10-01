using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Item_Code_management_System.Models
{
    public class UserItemCodeMapping
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [ForeignKey("Product")]
        public int? ItemCodeId { get; set; }  // Foreign Key

        [Required]
        [MaxLength(100)]
        public string UserItemCode { get; set; }  // User-defined Item Code
            
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UserPrice { get; set; }  // User-defined Price
        public virtual Product Product { get; set; }
    }
}
