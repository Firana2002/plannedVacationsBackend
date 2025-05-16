using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class VacationType
{
    [Key]
    public int VacationTypeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("VacationType")]
    public virtual ICollection<EmployeeVacationDay> EmployeeVacationDays { get; set; } = new List<EmployeeVacationDay>();

    [InverseProperty("VacationType")]
    public virtual ICollection<PlannedVacation> PlannedVacations { get; set; } = new List<PlannedVacation>();
}
