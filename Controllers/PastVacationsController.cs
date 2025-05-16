using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastVacationsController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public PastVacationsController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        // GET: api/PastVacations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PastVacations>>> GetPastVacations()
        {
            return await _context.PastVacations
                .Include(pv => pv.Employee)
                .Include(pv => pv.VacationType)
                .ToListAsync();
        }

        // GET: api/PastVacations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PastVacations>> GetPastVacation(int id)
        {
            var pastVacation = await _context.PastVacations
                .Include(pv => pv.Employee)
                .Include(pv => pv.VacationType)
                .FirstOrDefaultAsync(pv => pv.PastVacationId == id);

            return pastVacation ?? (ActionResult<PastVacations>)NotFound();
        }

        // POST: api/PastVacations
        [HttpPost]
        public async Task<ActionResult<PastVacations>> PostPastVacation(PastVacations pastVacation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.Employees.AnyAsync(e => e.EmployeeId == pastVacation.EmployeeId))
                return BadRequest("Employee not found");

            if (!await _context.VacationTypes.AnyAsync(vt => vt.VacationTypeId == pastVacation.VacationTypeId))
                return BadRequest("Vacation Type not found");

            _context.PastVacations.Add(pastVacation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPastVacation", new { id = pastVacation.PastVacationId }, pastVacation);
        }

        // PUT: api/PastVacations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPastVacation(int id, PastVacations pastVacation)
        {
            if (id != pastVacation.PastVacationId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.Employees.AnyAsync(e => e.EmployeeId == pastVacation.EmployeeId))
                return BadRequest("Employee not found");

            if (!await _context.VacationTypes.AnyAsync(vt => vt.VacationTypeId == pastVacation.VacationTypeId))
                return BadRequest("Vacation Type not found");

            _context.Entry(pastVacation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastVacationExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/PastVacations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePastVacation(int id)
        {
            var pastVacation = await _context.PastVacations.FindAsync(id);
            if (pastVacation == null)
                return NotFound();

            _context.PastVacations.Remove(pastVacation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PastVacationExists(int id)
        {
            return _context.PastVacations.Any(pv => pv.PastVacationId == id);
        }
    }
}