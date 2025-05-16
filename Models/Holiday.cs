using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class Holiday
{
    [Key]
    public int HolidayId { get; set; }

    public DateOnly Date { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
}
