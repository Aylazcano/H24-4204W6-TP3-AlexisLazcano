using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace flappyBirb_server.Models
{
    public class BirbUser : IdentityUser
    {
        // Propriétée de navigation
        public virtual List<Score> Scores { get; set; } = null!;
    }
}
