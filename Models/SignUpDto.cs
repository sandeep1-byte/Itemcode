using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Item_Code_management_System.Models
{
    public class SignUpDto
    {

        [Required]
        [MaxLength(50)]  
        public string Name { get; set; }


        [Required]  
        [MaxLength(50)]  
        public string Email { get; set; }

        [Required]  
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
