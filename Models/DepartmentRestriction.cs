using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class DepartmentRestriction
{
    [Key]
    public int DepartmentRestrictionId { get; set; }

    public int DepartmentId { get; set; }

    public int RestrictionId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("DepartmentRestrictions")]
    public virtual Department Department { get; set; } = null!;

    [ForeignKey("RestrictionId")]
    [InverseProperty("DepartmentRestrictions")]
    public virtual Restriction Restriction { get; set; } = null!;
}
