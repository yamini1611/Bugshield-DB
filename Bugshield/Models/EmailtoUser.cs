using System.ComponentModel.DataAnnotations;

namespace Bugshield.Models
{
    public class EmailtoUser
    {
        [Required]
        public required string FromEmail { get; set; }
        [Required]
        public  required  string ToEmail { get; set; }
        public required string Query { get; set; }
        [Required]

        public required string Password { get; set; }

        public DateTime SolvedTime { get; set; }

        public required string Progress { get; set; }


    }
}
