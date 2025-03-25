using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.Data.Models;

[Keyless]
[Table("TeamDetails_Archive")]
public partial class TeamDetails_Archive
{
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string TeamName { get; set; } = null!;

    public int Captain_Player_Id { get; set; }

    public string Season_Year { get; set; } = null!;
}
