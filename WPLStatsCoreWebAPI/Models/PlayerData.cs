using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.API.Models
{
    [PrimaryKey(nameof(PlayerId))]
    public class PlayerData
    {
        public int PlayerId { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int WeekNumber { get; set; }
    }
}
