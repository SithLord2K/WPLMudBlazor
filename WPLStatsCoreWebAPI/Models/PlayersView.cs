namespace WPLBlazor.API.Models
{
    public class PlayersView
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int GamesPlayed { get; set; }
        public int WeekNumber { get; set; }
    }
}
