using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace flappyBirb_server.Models
{
    public class Score
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

        // Propriétée de navigation
        [JsonIgnore]
        public virtual BirbUser? BirbUser { get; set; }
    }
}
