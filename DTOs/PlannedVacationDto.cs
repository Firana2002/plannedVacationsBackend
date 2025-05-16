using System;

namespace VacationPlanner.Api.DTOs
{
    public class PlannedVacationDto
    {
        public int EmployeeId { get; set; }
        public int VacationTypeId { get; set; }
        public int VacationStatusId { get; set; }
        public DateOnly StartDate { get; set; } // Изменено на DateOnly
        public DateOnly EndDate { get; set; }   // Изменено на DateOnly
        public string Comment { get; set; } = string.Empty;
    }
}
