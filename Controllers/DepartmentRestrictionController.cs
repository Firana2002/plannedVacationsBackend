using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentRestrictionsController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public DepartmentRestrictionsController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentRestrictions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentRestriction>>> GetDepartmentRestrictions()
        {
            return await _context.DepartmentRestrictions
                .Include(dr => dr.Department)
                .Include(dr => dr.Restriction)
                .ToListAsync();
        }

        // GET: api/DepartmentRestrictions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentRestriction>> GetDepartmentRestriction(int id)
        {
            var departmentRestriction = await _context.DepartmentRestrictions
                .Include(dr => dr.Department)
                .Include(dr => dr.Restriction)
                .FirstOrDefaultAsync(dr => dr.DepartmentRestrictionId == id);

            return departmentRestriction ?? (ActionResult<DepartmentRestriction>)NotFound();
        }

        // POST: api/DepartmentRestrictions
        [HttpPost]
        public async Task<ActionResult<DepartmentRestriction>> PostDepartmentRestriction(DepartmentRestriction departmentRestriction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.Departments.AnyAsync(d => d.DepartmentId == departmentRestriction.DepartmentId))
                return BadRequest("Department not found");

            if (!await _context.Restrictions.AnyAsync(r => r.RestrictionId == departmentRestriction.RestrictionId))
                return BadRequest("Restriction not found");

            _context.DepartmentRestrictions.Add(departmentRestriction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentRestriction", 
                new { id = departmentRestriction.DepartmentRestrictionId }, departmentRestriction);
        }

        // PUT: api/DepartmentRestrictions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentRestriction(int id, DepartmentRestriction departmentRestriction)
        {
            if (id != departmentRestriction.DepartmentRestrictionId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.Departments.AnyAsync(d => d.DepartmentId == departmentRestriction.DepartmentId))
                return BadRequest("Department not found");

            if (!await _context.Restrictions.AnyAsync(r => r.RestrictionId == departmentRestriction.RestrictionId))
                return BadRequest("Restriction not found");

            _context.Entry(departmentRestriction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentRestrictionExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/DepartmentRestrictions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentRestriction(int id)
        {
            var departmentRestriction = await _context.DepartmentRestrictions.FindAsync(id);
            if (departmentRestriction == null)
                return NotFound();

            _context.DepartmentRestrictions.Remove(departmentRestriction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentRestrictionExists(int id)
        {
            return _context.DepartmentRestrictions.Any(e => e.DepartmentRestrictionId == id);
        }
    }
}