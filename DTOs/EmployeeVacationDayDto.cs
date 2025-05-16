namespace VacationPlanner.Api.DTOs
{
    public class EmployeeVacationDayDto
    {
        public int EmployeeId { get; set; }
        public int VacationTypeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}