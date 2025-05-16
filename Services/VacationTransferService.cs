using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using VacationPlanner.Api.Models;

namespace VacationPlanner.Api.Services
{
    public class VacationTransferService
    {
        private readonly IServiceProvider _serviceProvider;

        public VacationTransferService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task TransferVacations()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<VacationPlannerDbContext>();
                 if (DateOnly.FromDateTime(DateTime.Now) != new DateOnly(DateTime.Now.Year, 1, 1))
            return; 

                if (DateOnly.FromDateTime(DateTime.Now) != new DateOnly(DateTime.Now.Year, 1, 1))
                    throw new InvalidOperationException("This action can only be performed on January 1st.");

                var pastVacations = await dbContext.EmployeeVacationDays
                    .Where(evd => evd.EndDate < new DateOnly(DateTime.Now.Year, 1, 1))
                    .ToListAsync();

                foreach (var vacation in pastVacations)
                {
                    var pastVacation = new PastVacations
                    {
                        EmployeeId = vacation.EmployeeId,
                        VacationTypeId = vacation.VacationTypeId,
                        StartDate = vacation.StartDate,
                        EndDate = vacation.EndDate
                    };
                    dbContext.PastVacations.Add(pastVacation);
                }

                dbContext.EmployeeVacationDays.RemoveRange(pastVacations);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
