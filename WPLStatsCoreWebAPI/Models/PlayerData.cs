using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.API.Models
{
    public class PlayerData
    {
        public int ID { get; set; }
        public int PlayerId { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int GamesPlayed { get; set; }
        public int WeekNumber { get; set; }
    }
}
