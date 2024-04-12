using Microsoft.EntityFrameworkCore;

namespace flappyBirb_server.Data
{
    public class FlappyBirbContext : DbContext
    {
        public FlappyBirbContext(DbContextOptions<FlappyBirbContext> options)
            : base(options)
        {
        }
        public DbSet<flappyBirb_server.Models.BirbUser> BirbUser { get; set; }
        public DbSet<flappyBirb_server.Models.Score> Score { get; set; }
    }
}
