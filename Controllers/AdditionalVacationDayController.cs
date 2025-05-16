using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalVacationDaysController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public AdditionalVacationDaysController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalVacationDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalVacationDay>>> GetAdditionalVacationDays()
        {
            return await _context.AdditionalVacationDays.ToListAsync();
        }

        // GET: api/AdditionalVacationDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalVacationDay>> GetAdditionalVacationDay(int id)
        {
            var additionalVacationDay = await _context.AdditionalVacationDays.FindAsync(id);

            if (additionalVacationDay == null)
            {
                return NotFound();
            }

            return additionalVacationDay;
        }

        // POST: api/AdditionalVacationDays
        [HttpPost]
        public async Task<ActionResult<AdditionalVacationDay>> PostAdditionalVacationDay(AdditionalVacationDay additionalVacationDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AdditionalVacationDays.Add(additionalVacationDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalVacationDay", 
                new { id = additionalVacationDay.AdditionalVacationDaysId }, additionalVacationDay);
        }

        // PUT: api/AdditionalVacationDays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalVacationDay(int id, AdditionalVacationDay additionalVacationDay)
        {
            if (id != additionalVacationDay.AdditionalVacationDaysId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(additionalVacationDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalVacationDayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AdditionalVacationDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalVacationDay(int id)
        {
            var additionalVacationDay = await _context.AdditionalVacationDays.FindAsync(id);
            if (additionalVacationDay == null)
            {
                return NotFound();
            }

            _context.AdditionalVacationDays.Remove(additionalVacationDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalVacationDayExists(int id)
        {
            return _context.AdditionalVacationDays.Any(e => e.AdditionalVacationDaysId == id);
        }
    }
}