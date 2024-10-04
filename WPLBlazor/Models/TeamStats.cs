namespace WPLBlazor.Models
{
    public class TeamStats
    {
        public int TeamId { get; set; }
        public int Week_Id { get; set; }
        public string? TeamName { get; set; }
        public int TotalGamesWon { get; set; }
        public int TotalGamesLost { get; set; }
        public int TotalGamesPlayed { get; set; }
        public decimal TotalAverage { get; set; }
        public int WeeksPlayed { get; set; }
    }
}
