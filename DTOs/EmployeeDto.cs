using System.ComponentModel.DataAnnotations; 

namespace VacationPlanner.Api.Dtos
{
    public class CreateEmployeeDto
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int AccumulatedVacationDays { get; set; }

        public bool? IsMultipleChildren { get; set; }
        public bool? HasDisabledChild { get; set; }
        public bool? IsVeteran { get; set; }
        public bool? IsHonorDonor { get; set; }
    }
}