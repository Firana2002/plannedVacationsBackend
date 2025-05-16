using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class VacationStatusSeeder
{
    private readonly VacationPlannerDbContext _context;

    public VacationStatusSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.VacationStatuses.Any())
        {
            return; // База данных уже заполнена
        }

        var vacationStatuses = new List<VacationStatus>
        {
            new VacationStatus { Name = "Запланирован" },
            new VacationStatus { Name = "Подтвержден" },
            new VacationStatus { Name = "Отменен" },
            new VacationStatus { Name = "Завершен" },
        };

        _context.VacationStatuses.AddRange(vacationStatuses);
        _context.SaveChanges();
    }
}
