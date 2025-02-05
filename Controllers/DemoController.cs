using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.MyLoggings;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //strongly coupled
        private readonly IMyLoggings _myLoggings;

        public DemoController()
        {
            //--strongly coupled ; need to change here for making different instances
            //_myLoggings = new LogToFile();
            //_myLoggings = new LogToDB();
            _myLoggings = new LogToServerMemory();
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLoggings.Log("Index Login to file");
            return Ok();
        }

    
    }
}
