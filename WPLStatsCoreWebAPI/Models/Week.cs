namespace WPLBlazor.API.Models;

public partial class Week
{
    public int Id { get; set; }

    public int WeekNumber { get; set; }

    public bool WeekWon { get; set; }

    public DateTime? DatePlayed { get; set; }

    public int TeamPlayed { get; set; }

    public bool Home { get; set; }
    public bool Forfeit { get; set; }
    public bool Playoff { get; set; }
}
