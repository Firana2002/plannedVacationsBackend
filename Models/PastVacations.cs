using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationPlanner.Api.Models;

public partial class PastVacations
{
    [Key]
    public int PastVacationId { get; set; } // Уникальный идентификатор отпуска

    [Required]
    public int EmployeeId { get; set; } // Идентификатор сотрудника

    [Required]
    public int VacationTypeId { get; set; } // Идентификатор типа отпуска

    [Required]
    public DateOnly StartDate { get; set; } // Дата начала отпуска

    [Required]
    public DateOnly EndDate { get; set; } // Дата окончания отпуска

    // Навигационные свойства
    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee Employee { get; set; } = null!; // Связь с моделью Employee

    [ForeignKey(nameof(VacationTypeId))]
    public virtual VacationType VacationType { get; set; } = null!; // Связь с моделью VacationType
}