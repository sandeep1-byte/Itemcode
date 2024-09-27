namespace Item_Code_management_System.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }  // Unique identifier for the user

        [Required]  // Name is required
        [MaxLength(50)]  // Name can be a maximum of 50 characters
        public string Name { get; set; }  // Name of the user

        [Required]  // Email is required
        [MaxLength(50)]  // Email can be a maximum of 50 characters
        public string Email { get; set; }  // Email of the user

        [Required]  // Password is required
        [MaxLength(50)]  // Password can be a maximum of 50 characters
        public string Password { get; set; }  // Password of the user
    }

}
