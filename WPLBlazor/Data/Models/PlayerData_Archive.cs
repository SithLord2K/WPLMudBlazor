using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WPLMudBlazor.Data.Models;

[Keyless]
[Table("PlayerData_Archive")]
public partial class PlayerData_Archive
{
    public int ID { get; set; }

    public int PlayerId { get; set; }

    public int GamesWon { get; set; }

    public int GamesLost { get; set; }

    public int GamesPlayed { get; set; }

    public int WeekNumber { get; set; }

    public string Season_Year { get; set; } = null!;
}
