using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace flappyBirb_server.Models
{
    public class BirbUser : IdentityUser
    {
        //public int Id { get; set; }
        //[Required]
        //public string Pseudo { get; set; }
        //[Required]
        //public string Password { get; set; }

        // Propriétée de navigation
        public virtual List<Score> Scores { get; set; } = null!;
    }
}
