namespace flappyBirb_server.Models
{
    public class Score
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TimeInSeconds { get; set; }
        public int ScoreValue { get; set; }
        public bool isPublic { get; set; }

        // Propriétée de navigation
        public virtual BirbUser? BirbUser { get; set; }
    }
}
