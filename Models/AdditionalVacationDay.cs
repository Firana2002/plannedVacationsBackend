using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class AdditionalVacationDay
{
    [Key]
    public int AdditionalVacationDaysId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int DaysCount { get; set; }
}
