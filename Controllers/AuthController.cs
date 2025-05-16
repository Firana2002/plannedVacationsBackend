using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VacationPlanner.Api.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowAllOrigins")]
public class AuthController : ControllerBase
{
    private readonly VacationPlannerDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(
        VacationPlannerDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserDto request)
    {
        // Найти сотрудника по email
        var employee = _context.Employees.FirstOrDefault(e => e.Email == request.Email);

        // Проверить существование сотрудника и корректность пароля
        if (employee == null || !BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
            return BadRequest("Invalid email or password");

        // Создать JWT-токен
        var token = CreateToken(employee);
        return Ok(token);
    }

    private string CreateToken(Employee employee)
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, employee.Email),
        new Claim("EmployeeId", employee.EmployeeId.ToString()),
        new Claim("RoleId", employee.RoleId.ToString()),
        new Claim("DepartmentId", employee.DepartmentId.ToString()) 
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddDays(_configuration.GetValue<double>("Jwt:ExpireDays")),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}

public class UserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

