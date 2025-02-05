using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.MyLoggings;

namespace WebAPI_Learn.Controllers
{
    //loosely coupled
    [Route("api/[controller]")]
    [ApiController]
    public class Demo2Controller : ControllerBase
    {
        private readonly IMyLoggings _myLoggings;

        public Demo2Controller(IMyLoggings myLoggings)
        {
            _myLoggings = myLoggings;
            //no need to make instance as required , depend upon AddScoped in Program.cs
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLoggings.Log("Index Login to file 2");
            return Ok();
        }
    }
}
