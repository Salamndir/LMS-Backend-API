using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Model
{
    // Represents the User table in the database
    [Table("Users")]
    public class User
    {
        [Key] // Marks this property as the Primary Key
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // "Admin" or "Trainee"
    }
}
