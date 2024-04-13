using System.ComponentModel.DataAnnotations;

namespace flappyBirbServer.Models
{
    public class RegisterDTO
    {
        [Required]
        public string username { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
