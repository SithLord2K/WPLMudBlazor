namespace WPLBlazor.API.Models;

public partial class TeamDetails
{
    public int Id { get; set; }

    public string TeamName { get; set; } = string.Empty;

    public int Captain_Player_Id { get; set; }
}
