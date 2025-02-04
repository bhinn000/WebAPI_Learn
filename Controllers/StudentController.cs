using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {

        [HttpGet]
        public string GetStudentName()
        {
            return "Student name is Vidhyarthi";
        }

    }
}
