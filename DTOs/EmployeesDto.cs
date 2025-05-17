
  // EmployeeDto.cs
public class EmployeesDto
{
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public int PositionId { get; set; }
    public int RoleId { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    
    public DateOnly HireDate { get; set; }
    public string Email { get; set; }
    
    public int AccumulatedVacationDays { get; set; }
    public int TotalAccumulatedVacationDays { get; set; }
    
   public bool? IsMultipleChildren { get; set; }
   public bool? HasDisabledChild { get; set; }
   public bool? IsVeteran { get; set; }
   public bool? IsHonorDonor { get; set; }
    
    // DTO для связанных сущностей
    public DepartmentDto Department { get; set; }
    public PositionDto Position { get; set; }
    public RoleDto Role { get; set; }
}


