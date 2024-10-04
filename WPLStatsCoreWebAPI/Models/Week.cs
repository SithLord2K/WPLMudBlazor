namespace WPLBlazor.API.Models;

public partial class Week
{
    public int Id { get; set; }
    public int WeekNumber { get; set; }
    public bool Home_Won { get; set; }
    public int Home_Team { get; set; }
    public int Away_Team { get; set; }
    public bool Forfeit { get; set; }
    public bool Playoff { get; set; }
}
