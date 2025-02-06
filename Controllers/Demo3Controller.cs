using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Demo3Controller: ControllerBase
    {
        private readonly ILogger<Demo3Controller> _logger;
        public Demo3Controller(ILogger<Demo3Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogTrace("Logging from Trace Level");
            _logger.LogDebug("Logging from Debug Level");
            _logger.LogInformation("Logging from Information Level");
            _logger.LogCritical("Logging from Critical Level");
            _logger.LogWarning("Logging from Warning Level");

            return Ok();
        }
    }
}
