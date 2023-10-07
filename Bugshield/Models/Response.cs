using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bugshield.Models
{
   public class Response
    {
        [Required]
        public  required string Status { set; get; }
            public required string Message { set; get; }
            public  required string Email { set; get; }
            public required string Phone { set; get; }
            public required string Username { set; get;}
            public int Computerid { set; get; }     
            public int Roleid { set; get; }
            public int Userid { set; get; }
            public required string Token { set; get; }
            public required string Password { set; get; }
             
    }
}
