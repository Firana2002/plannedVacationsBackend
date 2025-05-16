using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VacationPlanner.Api.Services;

namespace VacationPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacationTransferController : ControllerBase
    {
        private readonly ILogger<VacationTransferController> _logger;

        public VacationTransferController(ILogger<VacationTransferController> logger)
        {
            _logger = logger;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            _logger.LogInformation("Vacation Transfer Service is running.");
            return Ok(new { Status = "Running" });
        }
    }
}
