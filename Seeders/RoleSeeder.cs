using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class RoleSeeder
{
    private readonly VacationPlannerDbContext _context;

    public RoleSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Roles.Any())
        {
            return; // База данных уже заполнена
        }

        var roles = new List<Role>
            {
                new Role { NameRole = "Руководитель" },
                new Role { NameRole = "Сотрудник" }
            };

        _context.Roles.AddRange(roles);
        _context.SaveChanges();
    }
}
