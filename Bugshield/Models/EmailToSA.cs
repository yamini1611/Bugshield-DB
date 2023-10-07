using System.ComponentModel.DataAnnotations;

namespace Bugshield.Models
{
    public class EmailToSA
    {
        [Required]
        public required string FromEmail { get; set; }
        [Required]

        public required string ToEmail { get; set; }
      
    }
}
