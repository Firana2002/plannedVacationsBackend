using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class Restriction
{
    [Key]
    public int RestrictionId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string Type { get; set; } = null!;

    [InverseProperty("Restriction")]
    public virtual ICollection<DepartmentRestriction> DepartmentRestrictions { get; set; } = new List<DepartmentRestriction>();
}
