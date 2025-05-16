using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class DepartmentSeeder
{
    private readonly VacationPlannerDbContext _context;

    public DepartmentSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Departments.Any()) return;

        var departments = new List<Department>
        {
            new Department 
            { 
                Name = "Отдел кадров",
                OrganizationId = 1,
                
            },
            new Department 
            { 
                Name = "Финансовый отдел",
                OrganizationId = 1,
                
            },
            new Department 
            { 
                Name = "IT-отдел",
                OrganizationId = 1,
                
            },
            new Department 
            { 
                Name = "Маркетинг",
                OrganizationId = 1,
                
            },
            new Department 
            { 
                Name = "Отдел разработки",
                OrganizationId = 1,
                
            },
            new Department 
            { 
                Name = "Техническая поддержка",
                OrganizationId = 1,
                
            }
        };

        _context.Departments.AddRange(departments);
        _context.SaveChanges();
    }
}
