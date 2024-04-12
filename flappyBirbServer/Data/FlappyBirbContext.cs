using flappyBirb_server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace flappyBirb_server.Data
{
    public class FlappyBirbContext : IdentityDbContext<BirbUser>
    {
        public FlappyBirbContext(DbContextOptions<FlappyBirbContext> options)
            : base(options)
        {
        }
        public DbSet<flappyBirb_server.Models.Score> Score { get; set; } = default!;
    }
}
