namespace WPLBlazor.API.Models;

public partial class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int TeamId { get; set; }
}
