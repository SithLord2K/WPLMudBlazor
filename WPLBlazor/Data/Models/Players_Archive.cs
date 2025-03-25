using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WPLBlazor.Data.Models;

[Keyless]
[Table("Players_Archive")]
public partial class Players_Archive
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int TeamId { get; set; }

    public string Season_Year { get; set; } = null!;
}
