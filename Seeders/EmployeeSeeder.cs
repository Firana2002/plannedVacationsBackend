using VacationPlanner.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;

public class EmployeeSeeder
{
    private readonly VacationPlannerDbContext _context;

    public EmployeeSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        var seedEmployees = GetSeedEmployees();
        
        foreach (var seedEmployee in seedEmployees)
        {
            var existingEmployee = _context.Employees
                .FirstOrDefault(e => e.Email == seedEmployee.Email);

            if (existingEmployee != null)
            {
                // Обновляем изменяемые поля
                existingEmployee.DepartmentId = seedEmployee.DepartmentId;
                existingEmployee.PositionId = seedEmployee.PositionId;
                existingEmployee.RoleId = seedEmployee.RoleId;
                existingEmployee.FirstName = seedEmployee.FirstName;
                existingEmployee.LastName = seedEmployee.LastName;
                existingEmployee.MiddleName = seedEmployee.MiddleName;
                existingEmployee.PasswordHash = seedEmployee.PasswordHash;
                existingEmployee.AccumulatedVacationDays = seedEmployee.AccumulatedVacationDays;
                existingEmployee.IsMultipleChildren = seedEmployee.IsMultipleChildren;
                existingEmployee.HasDisabledChild = seedEmployee.HasDisabledChild;
                existingEmployee.IsVeteran = seedEmployee.IsVeteran;
                existingEmployee.IsHonorDonor = seedEmployee.IsHonorDonor;
            }
            else
            {
                _context.Employees.Add(seedEmployee);
            }
        }

        _context.SaveChanges();
    }

    private List<Employee> GetSeedEmployees()
    {
        return new List<Employee>
        {
            // Руководители отделов (RoleId = 1)
            new Employee
            {
                DepartmentId = 1, // Отдел кадров
                PositionId = 1,   // HR-менеджер
                RoleId = 1,
                FirstName = "Ольга",
                LastName = "Петрова",
                MiddleName = "Сергеевна",
                HireDate = DateOnly.FromDateTime(new DateTime(2020, 1, 15)),
                Email = "hr.manager@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrPassword123!"),
                AccumulatedVacationDays = 28,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = true
            },
            new Employee
            {
                DepartmentId = 2, // Финансовый отдел
                PositionId = 2,   // Финансовый директор
                RoleId = 1,
                FirstName = "Андрей",
                LastName = "Смирнов",
                MiddleName = "Игоревич",
                HireDate = DateOnly.FromDateTime(new DateTime(2019, 5, 10)),
                Email = "finance.director@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("FinancePass456!"),
                AccumulatedVacationDays = 30,
                IsMultipleChildren = true,
                HasDisabledChild = false,
                IsVeteran = true,
                IsHonorDonor = false
            },
            
            // Обычные сотрудники (RoleId = 2)
            new Employee
            {
                DepartmentId = 1,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Ирина",
                LastName = "Соколова",
                MiddleName = "Андреевна",
                HireDate = DateOnly.FromDateTime(new DateTime(2023, 2, 14)),
                Email = "hr.specialist@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrSpecialist123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },

             new Employee
            {
                DepartmentId = 1,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Марина",
                LastName = "Тестовая",
                MiddleName = "Кадрова",
                HireDate = DateOnly.FromDateTime(new DateTime(2024, 2, 14)),
                Email = "specialist@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrSpecialist123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },

             new Employee
            {
                DepartmentId = 1,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Ольга",
                LastName = "Олеговна",
                MiddleName = "Тестовская",
                HireDate = DateOnly.FromDateTime(new DateTime(2024, 5, 10)),
                Email = "spec@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrSpec123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },

             new Employee
            {
                DepartmentId = 1,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Антонина",
                LastName = "Антоновна",
                MiddleName = "Тестовская",
                HireDate = DateOnly.FromDateTime(new DateTime(2024, 6, 15)),
                Email = "spec@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Hr554123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },

            new Employee
            {
                DepartmentId = 1,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Наталья",
                LastName = "Олеговна",
                MiddleName = "Тестовская",
                HireDate = DateOnly.FromDateTime(new DateTime(2024, 6, 10)),
                Email = "spec@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrSpec123!12"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },

             new Employee
            {
                DepartmentId = 2,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Олег",
                LastName = "Финансовый",
                MiddleName = "Тестович",
                HireDate = DateOnly.FromDateTime(new DateTime(2023, 2, 14)),
                Email = "fi.@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("HrSpecialist123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },
                new Employee
            {
                DepartmentId = 2,
                PositionId = 5,   // Специалист по кадрам
                RoleId = 2,
                FirstName = "Алиса",
                LastName = "Финансовый",
                MiddleName = "Мозг",
                HireDate = DateOnly.FromDateTime(new DateTime(2023, 2, 14)),
                Email = "fiss1.@company.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("list123!"),
                AccumulatedVacationDays = 20,
                IsMultipleChildren = false,
                HasDisabledChild = false,
                IsVeteran = false,
                IsHonorDonor = false
            },
            
        };
 
    }
}