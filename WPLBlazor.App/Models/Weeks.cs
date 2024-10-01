namespace WPLBlazor.App.Models
{
    public class Weeks
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int WeekWin { get; set; }
        public int WeekLoss { get; set; }
        public bool Forfeit { get; set; }
        public bool WeekWon { get; set; }
        public int WeeksPlayed { get; set; }
        public decimal WeeksAverage { get; set; }
        public int TeamPlayed { get; set; }
        public bool Home { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
