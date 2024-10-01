using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Item_Code_management_System.Models
{
    public class UserCodeMappingDto
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [ForeignKey("Product")] // Foreign key for ItemCode
        public int? ItemCodeId { get; set; }

        [MaxLength(50)]
        public string UserItemCode { get; set; }  // User-defined Item Code

        [Range(0.01, double.MaxValue)]
        public decimal UserPrice { get; set; }  // User-defined Price

        public string ItemCode { get; set; }  // Add this property to hold the ItemCode value

        public IEnumerable<SelectListItem>? AvailableItemCodes { get; set; }  // List of available item codes for selection
    }

}
