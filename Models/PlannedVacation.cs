using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class PlannedVacation
{
    [Key]
    public int PlannedVacationId { get; set; }

    public int EmployeeId { get; set; }

    public int VacationTypeId { get; set; }

    public int VacationStatusId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("PlannedVacations")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("VacationTypeId")]
    [InverseProperty("PlannedVacations")]
    public virtual VacationType VacationType { get; set; } = null!;
    
    [ForeignKey("VacationStatusId")]
    [InverseProperty("PlannedVacations")]
    public virtual VacationStatus VacationStatus { get; set; } = null!;
}