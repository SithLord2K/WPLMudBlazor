namespace WPLBlazor.Models
{
    public class WeekFullInfo
    {
        public int WeekNumber { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public decimal Average { get; set; }
        public int Home_Team { get; set; }
        public int Away_Team { get; set; }
        public string? Home_TeamName { get; set; }
        public string? Away_TeamName { get; set; }
        public bool Home_Won { get; set; }
        public bool Forfeit { get; set; }
        public bool Playoff { get; set; }
    }
}
