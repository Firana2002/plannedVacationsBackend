using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public HolidaysController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetHolidays()
        {
            return await _context.Holidays.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Holiday>> GetHoliday(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            return holiday ?? (ActionResult<Holiday>)NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Holiday>> PostHoliday(Holiday holiday)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (await _context.Holidays.AnyAsync(h => h.Date == holiday.Date))
                return BadRequest("Holiday for this date already exists");

            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoliday", new { id = holiday.HolidayId }, holiday);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoliday(int id, Holiday holiday)
        {
            if (id != holiday.HolidayId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(holiday).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null) return NotFound();

            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HolidayExists(int id) => _context.Holidays.Any(e => e.HolidayId == id);
    }
}