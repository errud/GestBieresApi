using System.ComponentModel.DataAnnotations;

namespace GestBieresApi.Models
{
    public class LoginForm
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
