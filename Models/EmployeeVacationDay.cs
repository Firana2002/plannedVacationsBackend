using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models
{
    public partial class EmployeeVacationDay
    {
        [Key]
        public int EmployeeVacationDaysId { get; set; }

        public int EmployeeId { get; set; }

        public int VacationTypeId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeVacationDays")]
        public virtual Employee Employee { get; set; } = null!;

        [ForeignKey("VacationTypeId")]
        [InverseProperty("EmployeeVacationDays")]
        public virtual VacationType VacationType { get; set; } = null!;
    }
}