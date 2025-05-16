using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationTypesController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public VacationTypesController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacationType>>> GetVacationTypes()
        {
            return await _context.VacationTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacationType>> GetVacationType(int id)
        {
            var vacationType = await _context.VacationTypes.FindAsync(id);
            return vacationType ?? (ActionResult<VacationType>)NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<VacationType>> PostVacationType(VacationType vacationType)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _context.VacationTypes.Add(vacationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacationType", new { id = vacationType.VacationTypeId }, vacationType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacationType(int id, VacationType vacationType)
        {
            if (id != vacationType.VacationTypeId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(vacationType).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacationTypeExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacationType(int id)
        {
            var vacationType = await _context.VacationTypes
                .Include(vt => vt.EmployeeVacationDays)
                .Include(vt => vt.PlannedVacations)
                .FirstOrDefaultAsync(vt => vt.VacationTypeId == id);

            if (vacationType == null) return NotFound();
            if (vacationType.EmployeeVacationDays.Any() || vacationType.PlannedVacations.Any())
                return Conflict("Cannot delete vacation type with associated records");

            _context.VacationTypes.Remove(vacationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacationTypeExists(int id) => _context.VacationTypes.Any(e => e.VacationTypeId == id);
    }
}