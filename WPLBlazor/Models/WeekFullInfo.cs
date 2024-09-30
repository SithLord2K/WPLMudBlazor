namespace WPLBlazor.Models
{
    public class WeekFullInfo
    {
        public int WeekNumber { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public bool WeekWon { get; set; }
        public decimal Average { get; set; }
        public int TeamPlayed { get; set; }
        public string TeamName { get; set; } = "";
        public string? DatePlayed { get; set; }
        public bool Home { get; set; }
        public bool Forfeit { get; set; }
        public bool Playoff { get; set; }
    }
}
