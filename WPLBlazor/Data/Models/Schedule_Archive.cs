using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.Data.Models;

[Table("Schedule_Archive")]
public partial class Schedule_Archive
{
    [Key]
    public int Id { get; set; }

    public int Week_Id { get; set; }

    public DateOnly Date { get; set; }

    public int Home_Team { get; set; }

    public int Away_Team { get; set; }

    public int? Table_Number { get; set; }

    public bool? Playoffs { get; set; }

    public DateOnly? Playoff_Week_Id { get; set; }

    public int? Week_Id_Playoff { get; set; }

    public string Season_Year { get; set; } = null!;
}
