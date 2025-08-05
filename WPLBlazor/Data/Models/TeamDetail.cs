using Microsoft.EntityFrameworkCore;

namespace WPLMudBlazor.Data.Models;

[Keyless]
public partial class TeamDetail
{
    public int Id { get; set; }
    public string TeamName { get; set; } = null!;
    public int Captain_Player_Id { get; set; }
}
