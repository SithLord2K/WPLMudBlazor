namespace WPLBlazor.Models
{
    public class Weeks
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int WeekWin { get; set; }
        public int WeekLoss { get; set; }
        public bool Forfeit { get; set; }
        public bool Playoff { get; set; }
        public decimal WeeksAverage { get; set; }
        public int Home_Team { get; set; }
        public int Away_Team { get; set; }
        public bool Home_Won { get; set; }
    }
}
