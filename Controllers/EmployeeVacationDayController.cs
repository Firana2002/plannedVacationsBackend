using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;
using VacationPlanner.Api.DTOs;
using System.Security.Claims;

namespace VacationPlanner.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeVacationDaysController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public EmployeeVacationDaysController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        private int GetUserDepartmentId() => 
            int.Parse(User.FindFirst("DepartmentId")?.Value);

        // GET: api/EmployeeVacationDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeVacationDay>>> GetEmployeeVacationDays()
        {
            var departmentId = GetUserDepartmentId();
            
            return await _context.EmployeeVacationDays
                .Include(evd => evd.Employee)
                .Include(evd => evd.VacationType)
                .Where(evd => evd.Employee.DepartmentId == departmentId)
                .ToListAsync();
        }

        // GET: api/EmployeeVacationDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeVacationDay>> GetEmployeeVacationDay(int id)
        {
            var departmentId = GetUserDepartmentId();
            
            var employeeVacationDay = await _context.EmployeeVacationDays
                .Include(evd => evd.Employee)
                .Include(evd => evd.VacationType)
                .FirstOrDefaultAsync(evd => evd.EmployeeVacationDaysId == id);

            if (employeeVacationDay == null)
                return NotFound();

            if (employeeVacationDay.Employee.DepartmentId != departmentId)
                return Forbid("Доступ к записи другого отдела запрещен");

            return employeeVacationDay;
        }

        // POST: api/EmployeeVacationDays
        [HttpPost]
        public async Task<ActionResult<EmployeeVacationDay>> PostEmployeeVacationDay(EmployeeVacationDayDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentId = GetUserDepartmentId();
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == dto.EmployeeId);

            if (employee == null || employee.DepartmentId != departmentId)
                return BadRequest("Сотрудник не найден или принадлежит другому отделу");

            if (!await _context.VacationTypes.AnyAsync(vt => vt.VacationTypeId == dto.VacationTypeId))
                return BadRequest("Тип отпуска не найден");

            if (dto.StartDate > dto.EndDate)
                return BadRequest("Дата начала должна быть раньше даты окончания");

            var vacationDay = new EmployeeVacationDay
            {
                EmployeeId = dto.EmployeeId,
                VacationTypeId = dto.VacationTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            _context.EmployeeVacationDays.Add(vacationDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeVacationDay",
                new { id = vacationDay.EmployeeVacationDaysId }, vacationDay);
        }

        // PUT: api/EmployeeVacationDays/5
        [Authorize(Policy = "RequireManagerRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeVacationDay(int id, EmployeeVacationDayDto dto)
        {
            var departmentId = GetUserDepartmentId();
            var existingRecord = await _context.EmployeeVacationDays
                .Include(evd => evd.Employee)
                .FirstOrDefaultAsync(evd => evd.EmployeeVacationDaysId == id);

            if (existingRecord == null)
                return NotFound();

            if (existingRecord.Employee.DepartmentId != departmentId)
                return Forbid("Нет прав для изменения записей другого отдела");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.VacationTypes.AnyAsync(vt => vt.VacationTypeId == dto.VacationTypeId))
                return BadRequest("Тип отпуска не найден");

            if (dto.StartDate > dto.EndDate)
                return BadRequest("Дата начала должна быть раньше даты окончания");

            existingRecord.VacationTypeId = dto.VacationTypeId;
            existingRecord.StartDate = dto.StartDate;
            existingRecord.EndDate = dto.EndDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeVacationDayExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/EmployeeVacationDays/5
        [Authorize(Policy = "RequireManagerRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeVacationDay(int id)
        {
            var departmentId = GetUserDepartmentId();
            var vacationDay = await _context.EmployeeVacationDays
                .Include(evd => evd.Employee)
                .FirstOrDefaultAsync(evd => evd.EmployeeVacationDaysId == id);

            if (vacationDay == null)
                return NotFound();

            if (vacationDay.Employee.DepartmentId != departmentId)
                return Forbid("Нет прав для удаления записей другого отдела");

            _context.EmployeeVacationDays.Remove(vacationDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/EmployeeVacationDays/department/{departmentId}
        [Authorize(Policy = "RequireManagerRole")]
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<EmployeeVacationDay>>> GetEmployeeVacationDaysByDepartment(int departmentId)
        {
            var userDepartmentId = GetUserDepartmentId();
            
            if (departmentId != userDepartmentId)
                return Forbid("Доступ к данным других отделов запрещен");

            return await _context.EmployeeVacationDays
                .Include(evd => evd.Employee)
                .Include(evd => evd.VacationType)
                .Where(evd => evd.Employee.DepartmentId == departmentId)
                .ToListAsync();
        }

        private bool EmployeeVacationDayExists(int id)
        {
            return _context.EmployeeVacationDays.Any(e => e.EmployeeVacationDaysId == id);
        }
    }
}