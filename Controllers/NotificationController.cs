using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly VacationPlannerDbContext _context;

        public NotificationController(VacationPlannerDbContext context)
        {
            _context = context;
        }

        // Получить уведомления текущего пользователя
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetMyNotifications()
        {
            var employeeId = int.Parse(User.FindFirst("EmployeeId")!.Value);
            var roleId = int.Parse(User.FindFirst("RoleId")!.Value);
            
            var query = _context.Notifications
                .Where(n => n.EmployeeId == employeeId);

            // Если пользователь не руководитель (RoleId != 1), показываем только уведомления для сотрудников
            if (roleId != 1)
            {
                query = query.Where(n => !n.IsManagerNotification);
            }

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .Include(n => n.RelatedVacation)
                .ToListAsync();
        }

        // Получить уведомления для руководителя
        [Authorize(Policy = "RequireManagerRole")]
        [HttpGet("manager")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetManagerNotifications()
        {
            var employeeId = int.Parse(User.FindFirst("EmployeeId")!.Value);
            
            return await _context.Notifications
                .Where(n => n.EmployeeId == employeeId)
                .OrderByDescending(n => n.CreatedAt)
                .Include(n => n.RelatedVacation)
                .ToListAsync();
        }

        // Отметить уведомление как прочитанное
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var employeeId = int.Parse(User.FindFirst("EmployeeId")!.Value);
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == id && n.EmployeeId == employeeId);

            if (notification == null)
            {
                return NotFound();
            }

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Отметить все уведомления как прочитанные
        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var employeeId = int.Parse(User.FindFirst("EmployeeId")!.Value);
            
            var notifications = await _context.Notifications
                .Where(n => n.EmployeeId == employeeId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Получить количество непрочитанных уведомлений
        [HttpGet("unread-count")]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var employeeId = int.Parse(User.FindFirst("EmployeeId")!.Value);
            
            var count = await _context.Notifications
                .CountAsync(n => n.EmployeeId == employeeId && !n.IsRead);

            return count;
        }
    }
} 