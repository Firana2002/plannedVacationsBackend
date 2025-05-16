using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionsController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public RestrictionsController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restriction>>> GetRestrictions()
        {
            return await _context.Restrictions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restriction>> GetRestriction(int id)
        {
            var restriction = await _context.Restrictions.FindAsync(id);
            return restriction ?? (ActionResult<Restriction>)NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Restriction>> PostRestriction(Restriction restriction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _context.Restrictions.Add(restriction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestriction", new { id = restriction.RestrictionId }, restriction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestriction(int id, Restriction restriction)
        {
            if (id != restriction.RestrictionId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(restriction).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestrictionExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestriction(int id)
        {
            var restriction = await _context.Restrictions
                .Include(r => r.DepartmentRestrictions)
                .FirstOrDefaultAsync(r => r.RestrictionId == id);

            if (restriction == null) return NotFound();
            if (restriction.DepartmentRestrictions.Any())
                return Conflict("Cannot delete restriction with department associations");

            _context.Restrictions.Remove(restriction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestrictionExists(int id) => _context.Restrictions.Any(e => e.RestrictionId == id);
    }
}