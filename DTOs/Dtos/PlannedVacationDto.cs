// Dtos/PlannedVacationDto.cs
using System.ComponentModel.DataAnnotations;
namespace VacationPlanner.Api.Dtos;
// DTO для создания заявки
public class CreatePlannedVacationDto
{
    [Required]
    public int VacationTypeId { get; set; }
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    [Required]
    public DateOnly EndDate { get; set; }
    
    public string? Comment { get; set; }
}

// DTO для обновления статуса заявки
public class UpdateVacationStatusDto
{
    [Required]
    public int VacationStatusId { get; set; }
    
    public string? Comment { get; set; }
}