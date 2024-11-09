using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Dtos.Requests.Account
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}