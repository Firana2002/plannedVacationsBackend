using System;

namespace VacationPlanner.Api.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int EmployeeId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? RelatedVacationId { get; set; }
        public bool IsManagerNotification { get; set; }

        // Навигационные свойства
        public Employee Employee { get; set; }
        public PlannedVacation RelatedVacation { get; set; }
    }
} 