using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class VacationTypeSeeder
{
    private readonly VacationPlannerDbContext _context;

    public VacationTypeSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.VacationTypes.Any())
        {
            return; // База данных уже заполнена
        }

        var vacationTypes = new List<VacationType>
        {
            new VacationType { Name = "Ежегодный отпуск" },
            new VacationType { Name = "Больничный" },
            new VacationType { Name = "Отпуск по уходу за ребенком" },
            new VacationType { Name = "Неоплачиваемый отпуск" },
        };

        _context.VacationTypes.AddRange(vacationTypes);
        _context.SaveChanges();
    }
}
