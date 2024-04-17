using Microsoft.Build.Framework;

namespace flappyBirbServer.Models
{
    public class ScoreDTO
    {
        public int Id { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TimeInSeconds { get; set; }
        [Required]
        public int ScoreValue { get; set; }
        [Required]
        public bool IsPublic { get; set; }
    }
}
