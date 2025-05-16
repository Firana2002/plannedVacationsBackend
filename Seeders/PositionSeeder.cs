using VacationPlanner.Api.Models;
using System.Collections.Generic;
using System.Linq;

public class PositionSeeder
{
    private readonly VacationPlannerDbContext _context;

    public PositionSeeder(VacationPlannerDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Positions.Any())
        {
            return; // База данных уже заполнена
        }

        var positions = new List<Position>
        {
            new Position { Name = "Менеджер" },
            new Position { Name = "Разработчик" },
            new Position { Name = "Старший разработчик" },
            new Position { Name = "Тим-лид" },
            new Position { Name = "Аналитик" },
            new Position { Name = "Дизайнер" },
            new Position { Name = "Тестировщик" },
            new Position { Name = "HR-менеджер" },
            new Position { Name = "Маркетолог" },
            new Position { Name = "Системный администратор" },
            new Position { Name = "Архитектор ПО" },
            new Position { Name = "Бизнес-аналитик" },
        };

        _context.Positions.AddRange(positions);
        _context.SaveChanges();
    }
}
