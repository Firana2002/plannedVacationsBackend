using Microsoft.EntityFrameworkCore;
using VacationPlanner.Api.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace VacationPlanner.Api.Services 
{
    public class VacationDaysUpdateService : IHostedService, IDisposable
    {
        private readonly ILogger<VacationDaysUpdateService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer? _timer;

        public VacationDaysUpdateService(
            ILogger<VacationDaysUpdateService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vacation Days Update Service is starting.");

            // Запускаем таймер, который будет вызывать обновление каждый день
            _timer = new Timer(UpdateVacationDays, null, 0, (int)TimeSpan.FromDays(1).TotalMilliseconds);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vacation Days Update Service is stopping.");

            // Останавливаем таймер
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void UpdateVacationDays(object? state)
        {
            _ = UpdateVacationDaysAsync(CancellationToken.None); // Запускаем асинхронный метод
        }
        
        public async Task UpdateVacationDaysAsync(CancellationToken cancellationToken)
        {
            try 
            {
                Console.WriteLine("\n=== НАЧАЛО ОБНОВЛЕНИЯ ДНЕЙ ОТПУСКА ===");
                _logger.LogInformation("Starting vacation days update process...");

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<VacationPlannerDbContext>();
                    
                    var employees = await dbContext.Employees
                        .Include(e => e.PlannedVacations.Where(pv => pv.VacationStatusId == 2))
                        .ToListAsync(cancellationToken);

                    Console.WriteLine($"Найдено сотрудников для обновления: {employees.Count}");
                    _logger.LogInformation($"Found {employees.Count} employees to update");

                    foreach (var employee in employees)
                    {
                        var hireDate = employee.HireDate;
                        var currentDate = DateOnly.FromDateTime(DateTime.Now);

                        // Вычисляем количество лет, прошедших с даты трудоустройства
                        var yearsPassed = (currentDate.Year - hireDate.Year) + ((currentDate.Month - hireDate.Month) / 12.0) + ((currentDate.Day - hireDate.Day) / 365.0);
                        
                        // Рассчитываем количество полугодий
                        var halfYears = Math.Floor(yearsPassed * 2) / 2;
                        
                        // Вычисляем количество дней в одобренных заявках
                        var approvedDays = employee.PlannedVacations
                            .Where(pv => pv.VacationStatusId == 2)
                            .Sum(pv => CalculateDaysBetween(pv.StartDate, pv.EndDate));

                        // Рассчитываем общее количество накопленных дней
                        var totalAccumulatedDays = 0;
                        if (halfYears >= 0.5) // Если прошло хотя бы полгода
                        {
                            totalAccumulatedDays = (int)(halfYears * 14); // 14 дней за каждые полгода
                        }

                        // Обновляем значения
                        employee.TotalAccumulatedVacationDays = totalAccumulatedDays;
                        employee.AccumulatedVacationDays = totalAccumulatedDays - approvedDays;
                        
                        Console.WriteLine($"\nСотрудник: {employee.FirstName} {employee.LastName}");
                        Console.WriteLine($"Дата приема: {hireDate}");
                        Console.WriteLine($"Текущая дата: {currentDate}");
                        Console.WriteLine($"Прошло лет: {yearsPassed:F2}");
                        Console.WriteLine($"Полугодий: {halfYears:F2}");
                        Console.WriteLine($"Всего дней: {totalAccumulatedDays}");
                        Console.WriteLine($"Использовано дней: {approvedDays}");
                        Console.WriteLine($"Осталось дней: {employee.AccumulatedVacationDays}");
                        Console.WriteLine("----------------------------------------");
                        
                        _logger.LogInformation(
                            "Employee {EmployeeId} ({FirstName} {LastName}): " +
                            "Hire date: {HireDate}, " +
                            "Current date: {CurrentDate}, " +
                            "Years passed: {YearsPassed}, " +
                            "Half-years: {HalfYears}, " +
                            "Total days: {TotalDays}, " +
                            "Approved days: {ApprovedDays}, " +
                            "Remaining days: {RemainingDays}",
                            employee.EmployeeId,
                            employee.FirstName,
                            employee.LastName,
                            hireDate,
                            currentDate,
                            yearsPassed,
                            halfYears,
                            totalAccumulatedDays,
                            approvedDays,
                            employee.AccumulatedVacationDays
                        );
                        
                        dbContext.Update(employee);
                    }

                    var saveResult = await dbContext.SaveChangesAsync(cancellationToken);
                    Console.WriteLine($"\nУспешно обновлено записей: {saveResult}");
                    _logger.LogInformation($"Successfully updated {saveResult} employee records");
                }
                Console.WriteLine("=== ОБНОВЛЕНИЕ ДНЕЙ ОТПУСКА ЗАВЕРШЕНО ===\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОШИБКА: {ex.Message}");
                _logger.LogError(ex, "An error occurred while updating vacation days");
                throw;
            }
        }

        private int CalculateDaysBetween(DateOnly startDate, DateOnly endDate)
        {
            return (endDate.DayNumber - startDate.DayNumber) + 1;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}