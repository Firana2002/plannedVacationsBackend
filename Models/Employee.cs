using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationPlanner.Api.Models;

public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }

    public int PositionId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;
    
    [StringLength(50)]
    public string MiddleName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }
    
    public int AccumulatedVacationDays { get; set; }
    
    public int TotalAccumulatedVacationDays { get; set; }

    public bool? IsMultipleChildren { get; set; }
    public bool? HasDisabledChild { get; set; }
    public bool? IsVeteran { get; set; }
    public bool? IsHonorDonor { get; set; }

    // Навигационные свойства
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey(nameof(DepartmentId))]
    public virtual Department Department { get; set; } = null!;

    [ForeignKey(nameof(PositionId))]
    public virtual Position Position { get; set; } = null!;

    // Обратная связь для управления отделами
    [InverseProperty("Manager")]
    public virtual ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();

    public virtual ICollection<EmployeeVacationDay> EmployeeVacationDays { get; set; } = new List<EmployeeVacationDay>();
    public virtual ICollection<PlannedVacation> PlannedVacations { get; set; } = new List<PlannedVacation>();
}