using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new VacationPlannerDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<VacationPlannerDbContext>>());

            // Добавляем роли, если их нет
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { NameRole = "Руководитель" },
                    new Role { NameRole = "Сотрудник" }
                );
                await context.SaveChangesAsync();
            }

            // Добавляем отделы, если их нет
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "IT-отдел" },
                    new Department { Name = "HR-отдел" },
                    new Department { Name = "Финансовый отдел" }
                );
                await context.SaveChangesAsync();
            }

            // Добавляем должности, если их нет
            if (!context.Positions.Any())
            {
                context.Positions.AddRange(
                    new Position { Name = "Разработчик" },
                    new Position { Name = "HR-менеджер" },
                    new Position { Name = "Финансист" }
                );
                await context.SaveChangesAsync();
            }

            // Добавляем типы отпусков, если их нет
            if (!context.VacationTypes.Any())
            {
                context.VacationTypes.AddRange(
                    new VacationType { Name = "Основной отпуск" },
                    new VacationType { Name = "Дополнительный отпуск" },
                    new VacationType { Name = "Отпуск за свой счет" }
                );
                await context.SaveChangesAsync();
            }

            // Добавляем статусы отпусков, если их нет
            if (!context.VacationStatuses.Any())
            {
                context.VacationStatuses.AddRange(
                    new VacationStatus { Name = "В процессе" },
                    new VacationStatus { Name = "Одобрено" },
                    new VacationStatus { Name = "Отклонено" }
                );
                await context.SaveChangesAsync();
            }

            // Получаем ID для использования
            var managerRoleId = context.Roles.First(r => r.NameRole == "Руководитель").RoleId;
            var employeeRoleId = context.Roles.First(r => r.NameRole == "Сотрудник").RoleId;
            var itDepartmentId = context.Departments.First(d => d.Name == "IT-отдел").DepartmentId;
            var hrDepartmentId = context.Departments.First(d => d.Name == "HR-отдел").DepartmentId;
            var developerPositionId = context.Positions.First(p => p.Name == "Разработчик").PositionId;
            var hrManagerPositionId = context.Positions.First(p => p.Name == "HR-менеджер").PositionId;

            // Обновляем или добавляем руководителя IT-отдела
            var itManager = await context.Employees
                .FirstOrDefaultAsync(e => e.Email == "it.manager@company.com");

            if (itManager == null)
            {
                itManager = new Employee
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Иванович",
                    Email = "it.manager@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                    DepartmentId = itDepartmentId,
                    PositionId = developerPositionId,
                    RoleId = managerRoleId,
                    HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2)),
                    AccumulatedVacationDays = 56,
                    TotalAccumulatedVacationDays = 56
                };
                context.Employees.Add(itManager);
            }
            else
            {
                itManager.FirstName = "Иван";
                itManager.LastName = "Иванов";
                itManager.MiddleName = "Иванович";
                itManager.DepartmentId = itDepartmentId;
                itManager.PositionId = developerPositionId;
                itManager.RoleId = managerRoleId;
                itManager.HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
                itManager.AccumulatedVacationDays = 56;
                itManager.TotalAccumulatedVacationDays = 56;
                context.Employees.Update(itManager);
            }

            // Обновляем или добавляем руководителя HR-отдела
            var hrManager = await context.Employees
                .FirstOrDefaultAsync(e => e.Email == "hr.manager@company.com");

            if (hrManager == null)
            {
                hrManager = new Employee
                {
                    FirstName = "Петр",
                    LastName = "Петров",
                    MiddleName = "Петрович",
                    Email = "hr.manager@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                    DepartmentId = hrDepartmentId,
                    PositionId = hrManagerPositionId,
                    RoleId = managerRoleId,
                    HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-3)),
                    AccumulatedVacationDays = 70,
                    TotalAccumulatedVacationDays = 70
                };
                context.Employees.Add(hrManager);
            }
            else
            {
                hrManager.FirstName = "Петр";
                hrManager.LastName = "Петров";
                hrManager.MiddleName = "Петрович";
                hrManager.DepartmentId = hrDepartmentId;
                hrManager.PositionId = hrManagerPositionId;
                hrManager.RoleId = managerRoleId;
                hrManager.HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-3));
                hrManager.AccumulatedVacationDays = 70;
                hrManager.TotalAccumulatedVacationDays = 70;
                context.Employees.Update(hrManager);
            }

            // Обновляем или добавляем сотрудника IT-отдела
            var itEmployee = await context.Employees
                .FirstOrDefaultAsync(e => e.Email == "it.employee@company.com");

            if (itEmployee == null)
            {
                itEmployee = new Employee
                {
                    FirstName = "Алексей",
                    LastName = "Алексеев",
                    MiddleName = "Алексеевич",
                    Email = "it.employee@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee123!"),
                    DepartmentId = itDepartmentId,
                    PositionId = developerPositionId,
                    RoleId = employeeRoleId,
                    HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                    AccumulatedVacationDays = 28,
                    TotalAccumulatedVacationDays = 28
                };
                context.Employees.Add(itEmployee);
            }
            else
            {
                itEmployee.FirstName = "Алексей";
                itEmployee.LastName = "Алексеев";
                itEmployee.MiddleName = "Алексеевич";
                itEmployee.DepartmentId = itDepartmentId;
                itEmployee.PositionId = developerPositionId;
                itEmployee.RoleId = employeeRoleId;
                itEmployee.HireDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-1));
                itEmployee.AccumulatedVacationDays = 28;
                itEmployee.TotalAccumulatedVacationDays = 28;
                context.Employees.Update(itEmployee);
            }

            await context.SaveChangesAsync();
        }
    }
} 