using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.Data.Models;

[Table("Weeks_Archive")]
public partial class Weeks_Archive
{
    [Key]
    public int Id { get; set; }

    public int WeekNumber { get; set; }

    public int Away_Team { get; set; }

    public int Home_Team { get; set; }

    public bool Home_Won { get; set; }

    public bool Forfeit { get; set; }

    public bool Playoff { get; set; }

    public int? WinningTeamId { get; set; }

    public string Season_Year { get; set; } = null!;
}
