using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadebymeWF.Models
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; } 

        [Column("name")]
        public string Name { get; set; }

        [Column("mobile_number")]
        [MaxLength(13)] 
        public string MobileNumber { get; set; }

        [Column("email")]
        [MaxLength(40)] 
        public string Email { get; set; }

        [Column("login")]
        [Required]
        [MaxLength(30)] 
        public string Login { get; set; }

        [Column("password")]
        [Required]
        [MaxLength(255)] 
        public string Password { get; set; }
    }
}
