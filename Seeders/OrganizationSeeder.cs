using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class OrganizationSeeder
{
    private readonly VacationPlannerDbContext _context;

    public OrganizationSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Organizations.Any())
        {
            return; // База данных уже заполнена
        }

        var organizations = new List<Organization>
        {
            new Organization { Name = "En+ Digital" },
        };

        _context.Organizations.AddRange(organizations);
        _context.SaveChanges();
    }
}
