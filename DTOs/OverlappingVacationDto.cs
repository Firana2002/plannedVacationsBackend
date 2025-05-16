public class OverlappingVacationDto
{
    public string EmployeeName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public class VacationWarningDto
{
    public string Message { get; set; }
    public List<OverlappingVacationDto> OverlappingVacations { get; set; }
}