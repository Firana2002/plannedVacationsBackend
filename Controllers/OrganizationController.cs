using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public OrganizationsController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            return await _context.Organizations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            return organization ?? (ActionResult<Organization>)NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganization", new { id = organization.OrganizationId }, organization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization organization)
        {
            if (id != organization.OrganizationId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(organization).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var organization = await _context.Organizations
                .Include(o => o.Departments)
                .FirstOrDefaultAsync(o => o.OrganizationId == id);

            if (organization == null) return NotFound();
            if (organization.Departments.Any()) 
                return Conflict("Cannot delete organization with related departments");

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationExists(int id) => _context.Organizations.Any(e => e.OrganizationId == id);
    }
}