using System.ComponentModel.DataAnnotations;

namespace Bugshield.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
