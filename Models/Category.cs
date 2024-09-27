using System.ComponentModel.DataAnnotations;

namespace Item_Code_management_System.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
